using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Data_Providers_Contracts
{
    public interface ISpecialityDataProvider : IDataProvider<Speciality>
    {
        void Delete(string id);

        void ClearSalaries( );
    }
}
