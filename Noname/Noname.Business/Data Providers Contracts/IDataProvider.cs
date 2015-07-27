using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Data_Providers_Contracts
{
    public interface IDataProvider<T>
    {
        IList<T> GetList();

        T GetById(string id);

        void Create(T model);

        void Update(T model);

        void Delete(T model);
    }
}
