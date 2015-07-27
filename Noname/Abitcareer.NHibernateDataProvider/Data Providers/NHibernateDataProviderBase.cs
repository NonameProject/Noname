using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using Events.NHibernateDataProvider.NHibernateCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Profiling;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateDataProviderBase<TEntity>
        where TEntity : BaseModel
    {
        private ISession CreateSession()
        {
            var session = Helper.OpenSession();
            return session;
        }

        private T Execute<T>(Func<ISession, T> func, string funcName, string errorMessage = null)
        {
            try
            {
                using (MiniProfiler.Current.Step(funcName))
                {
                    using (var session = CreateSession())
                    {
                        return func(session);
                    }
                }                
            }
            catch (Exception)
            {
                //Log message here
                throw;
            }
        }

        private void Execute(Action<ISession> action, string funcName, string errorMessage = null)
        {
            try
            {
                using (MiniProfiler.Current.Step(funcName))
                {
                    using (var session = CreateSession())
                    {
                        action(session);
                    }
                }                
            }
            catch (Exception)
            {
                //Log message here
                throw;
            }
        }

        public IList<TEntity> GetList()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                return criteria.List<TEntity>();
            }, "GetList");
        }

        public TEntity GetById(string id)
        {
            return Execute(session =>
            {
                return session.Get<TEntity>(id);
            }, "GetById");
        }

        public void Create(TEntity model)
        {
            Execute(session =>
           {
               using (var transaction = session.BeginTransaction())
               {
                   session.Save(model);
                   transaction.Commit();
               }
           }, "Create");
        }

        public void Update(TEntity model)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(model);
                    session.Flush();
                }
            }, "Update");
        }

        public void Delete(TEntity model)
        {
            Execute(session =>
            {
                session.Delete(model);
                session.Flush();
            }, "Delete");
        }
    }
}
