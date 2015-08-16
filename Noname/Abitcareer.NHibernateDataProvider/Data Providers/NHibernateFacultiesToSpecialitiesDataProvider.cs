using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abitcareer.Business.Data_Providers_Contracts;

namespace Abitcareer.NHibernateDataProvider.Data_Providers
{
    public class NHibernateFacultiesToSpecialitiesDataProvider : NHibernateDataProviderBase<FacultyToSpeciality>, IFacultyToSpecialityDataProvider
    {
    }
}
