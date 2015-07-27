using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.Miniprofiler
{
    public class DbCommandProxy : RealProxy
    {
        private DbCommand instance;
        private IDbProfiler profiler;

        private DbCommandProxy(DbCommand instance)
            : base(typeof(DbCommand))
        {
            this.instance = instance;
            this.profiler = MiniProfiler.Current as IDbProfiler;
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage methodMessage = new MethodCallMessageWrapper((IMethodCallMessage)msg);

            var executeType = GetExecuteType(methodMessage);

            if (executeType != SqlExecuteType.None)
                profiler.ExecuteStart(instance, executeType);

            object returnValue = methodMessage.MethodBase.Invoke(instance, methodMessage.Args);

            if (executeType == SqlExecuteType.Reader)
                returnValue = new ProfiledDbDataReader((DbDataReader)returnValue, instance.Connection, profiler);

            IMessage returnMessage = new ReturnMessage(returnValue, methodMessage.Args, methodMessage.ArgCount, methodMessage.LogicalCallContext, methodMessage);

            if (executeType == SqlExecuteType.Reader)
                profiler.ExecuteFinish(instance, executeType, (DbDataReader)returnValue);
            else if (executeType != SqlExecuteType.None)
                profiler.ExecuteFinish(instance, executeType, (DbDataReader)returnValue);

            return returnMessage;
        }

        private static SqlExecuteType GetExecuteType(IMethodCallMessage message)
        {
            switch (message.MethodName)
            {
                case "ExecuteNonQuery":
                    return SqlExecuteType.NonQuery;
                case "ExecuteReader":
                    return SqlExecuteType.Reader;
                case "ExecuteScalar":
                    return SqlExecuteType.Scalar;
                default:
                    return SqlExecuteType.None;
            }
        }

        public static IDbCommand CreateProxy(IDbCommand instance)
        {
            var proxy = new DbCommandProxy(instance as DbCommand);

            return proxy.GetTransparentProxy() as IDbCommand;
        }
    }
}
