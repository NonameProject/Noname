using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Profiling;
using System.Data;

namespace Abitcareer.Business.Components.Miniprofiler
{
    public class ProfiledSqlCeDriver : NHibernate.Driver.SqlServerCeDriver
    {
        public override IDbCommand CreateCommand()
        {
            IDbCommand command = base.CreateCommand();

            if (MiniProfiler.Current != null)
                command = DbCommandProxy.CreateProxy(command);

            return command;
        }
    }
}
