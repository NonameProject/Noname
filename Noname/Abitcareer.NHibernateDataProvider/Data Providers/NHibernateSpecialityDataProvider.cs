using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateSpecialityDataProvider : NHibernateDataProviderBase<Speciality>, ISpecialityDataProvider
    {
        public Speciality GetByName(string name)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Speciality))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Speciality>();
            });
        }
    }
}
