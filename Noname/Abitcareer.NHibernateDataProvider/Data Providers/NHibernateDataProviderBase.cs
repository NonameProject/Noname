﻿using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using Events.NHibernateDataProvider.NHibernateCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Profiling;
using Abitcareer.Business.Components;
using Elmah;
using NHibernate.Criterion;
using System.Xml.Linq;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateDataProviderBase<TEntity>
        where TEntity : BaseModel
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

        public IList<TEntity> GetList()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<TEntity>();
                return criteria.List<TEntity>().OrderBy(x => x.Name).ToList();
            });
        }

        public TEntity GetById(string id)
        {
            return Execute(session =>
            {
                return session.Get<TEntity>(id);
            });
        }

        public TEntity GetByName(string name)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(TEntity))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<TEntity>();
            });
        }
      

        public void Create(TEntity model)
        {
            Execute(session =>
           {
               using (var transaction = session.BeginTransaction())
               {
                   session.SaveOrUpdate(model);
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
                    session.SaveOrUpdate(model);
                    session.Flush();
                    transaction.Commit();
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

        public void Delete(string id)
        {
            Execute(session =>
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(session.Load(typeof(Speciality), id));
                        //session.Flush();
                        transaction.Commit();
                    }

                });
        }
    }
}
