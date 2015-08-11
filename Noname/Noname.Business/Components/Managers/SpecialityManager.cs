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

        public bool Create(Speciality model)
        {
            ClearCache();
            try
            {
                provider.Create(model);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IList<Speciality> GetList() 
        {
            ClearCache();
            var list = provider.GetList();
            var newList = new List<Speciality>(list.Count);
            foreach (var item in list)
            {
                newList.Add((Speciality)GetBaseModel(item));
            }
            return newList;
        }

        public bool TrySave(Speciality editedModel)
        {
            bool result;
            try
            {
                if (String.IsNullOrEmpty(editedModel.EnglishName) || String.IsNullOrEmpty(editedModel.Name))
                    throw new ArgumentException("One of the names is unacceptable");
                provider.Update(editedModel);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool IsExists(Speciality model)
        {
            if (provider.GetByName(model.Name) != null)
                return true;
            return false;
        }

        public Speciality GetById(string id)
        {
            return provider.GetById(id);
        }
    }
}
