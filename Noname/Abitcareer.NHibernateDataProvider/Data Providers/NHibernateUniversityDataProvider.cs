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
    public class NHibernateUniversityDataProvider : NHibernateDataProviderBase<University>, IUniversityDataProvider
    {
    }
}
