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
    public class NHibernateFacultyDataProvider : NHibernateDataProviderBase<Faculty>, IFacultyDataProvider
    {
        public Faculty GetByName(string name)
        {
            return Execute(session =>
            {
                return session
                    .CreateCriteria(typeof(Faculty))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Faculty>();
            });
        }
    }
}
