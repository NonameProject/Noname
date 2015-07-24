using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Data_Providers_Contracts
{
    public interface IUniversityDataProvider
    {
        IList<University> GetList();

        University GetById(string id);

        void Create(University model, City cityModel);

        void Update(University model);

        void Delete(University model);
    }
}
