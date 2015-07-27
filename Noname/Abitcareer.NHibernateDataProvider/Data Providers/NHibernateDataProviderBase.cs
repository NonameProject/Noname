using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using Events.NHibernateDataProvider.NHibernateCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private T Execute<T>(Func<ISession, T> func, string errorMessage = null)
        {
            try
            {
                using (var session = CreateSession())
                {
                    return func(session);
                }
            }
            catch (Exception)
            {
                //Log message here
                throw;
            }
        }

        private void Execute(Action<ISession> action, string errorMessage = null)
        {
            try
            {
                using (var session = CreateSession())
                {
                    action(session);
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
            });
        }

        public TEntity GetById(string id)
        {
            return Execute(session =>
            {
                return session.Get<TEntity>(id);
            });
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
           });
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
            });
        }

        public void Delete(TEntity model)
        {
            Execute(session =>
            {
                session.Delete(model);
                session.Flush();
            });
        }
    }
}
