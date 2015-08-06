using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using Elmah;
using Events.NHibernateDataProvider.NHibernateCore;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateUserDataProvider : IUserDataProvider
    {
        private ISession CreateSession()
        {
            return Helper.OpenSession();
        }

        protected T Execute<T>(Func<ISession, T> func, string errorMessage = null)
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
                ErrorSignal
                    .FromCurrentContext()
                        .Raise(new Exception(errorMessage));
                throw;
            }
        }

        protected void Execute(Action<ISession> action, string errorMessage = null)
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
                ErrorSignal
                    .FromCurrentContext()
                        .Raise(new Exception(errorMessage));
                throw;
            }
        }

        public IList<User> GetList()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<User>();
                return criteria.List<User>().OrderBy(x => x.Email).ToList();
            });
        }

        public User GetById(string id)
        {
            return Execute(session =>
            {
                return session.Get<User>(id);
            });
        }

        public void Create(User model)
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

        public void Update(User model)
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

        public void Delete(User model)
        {
            Execute(session =>
            {
                session.Delete(model);
                session.Flush();
            });
        }

        public User GetByEmail(string email)
        {
            return Execute(session =>
                {
                    return session
                        .CreateCriteria<User>()
                        .Add(Restrictions.Eq("Email", email))
                        .UniqueResult<User>();
                });
        }
    }
}
