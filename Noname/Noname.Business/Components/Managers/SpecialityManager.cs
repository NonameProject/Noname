using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.Managers
{
    public class SpecialityManager : BaseManager
    {
        ISpecialityDataProvider provider;

        protected override string Name
        {
            get
            {
                return "Speciality";
            }

            set
            {
            }
        }

        public SpecialityManager(ICacheManager manager, ISpecialityDataProvider provider)
            : base(manager)
        {
            this.provider = provider;
        }

        public void Create(Speciality model)
        {
            ClearCache();
            provider.Create(model);
        }
        public IList<Speciality> GetList() 
        {
            ClearCache();
            return provider.GetList();
        }

        public bool TrySave(Speciality editedModel)
        {
            bool result;
            try
            {
                //code that will save changes
                throw new NotImplementedException();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
