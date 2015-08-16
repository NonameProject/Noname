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
    public class NHibernateUserDataProvider : NHibernateDataProviderBase<User>,  IUserDataProvider
    {
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
