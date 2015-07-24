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
    public class NHibernateUniversityDataProvider : IUniversityDataProvider
    {
        public IList<University> GetList()
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria<University>();
                return criteria.List<University>();
            }
        }

        public University GetById(string id)
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.Get<University>(id);
            }
        }

        public void Create(University model, City cityModel)
        {
            using (ISession session = Helper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    model.City = cityModel;
                    model.City.Universities.Add(model);
                    session.Save(model);
                    session.Save(model.City);
                    transaction.Commit();
                }
            }
        }

        public void Update(University model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Update(model);
                session.Flush();
            }
        }

        public void Delete(University model)
        {
            using (ISession session = Helper.OpenSession())
            {
                session.Delete(model);
                session.Flush();
            }
        }
    }
}
