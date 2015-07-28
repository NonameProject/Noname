using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateFacultyDataProvider : NHibernateDataProviderBase<Faculty>, IFacultyDataProvider
    {
    }
}
