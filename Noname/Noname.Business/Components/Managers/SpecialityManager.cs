using Abitcareer.Business.Components.Lucene;
using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.Business.Models;
using CultureEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.Managers
{
    public class SpecialityManager : BaseManager
    {
        ISpecialityDataProvider provider;

        private string luceneDirectory
        {
            get
            {
                return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "App_Data", CultureInfo.CurrentCulture.ToString(), "lucene_index");
            }
        }

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

        public void ClearSalaries( )
        {
            provider.ClearSalaries();
        }

        public IList<Speciality> GetList() 
        {   
            var  list = SortBasedOnCurrentCulture(provider.GetList(), (LCID)CultureInfo.CurrentCulture.LCID);
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

        public void Delete(string id)
        {
            ClearCache();
            if(provider.GetById(id) != null)
                provider.Delete(id);
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

        public void Index()
        {
            var list = this.GetList();

            var languageSegment = CultureInfo.CurrentCulture;

            var searcher = new MySearcher<Speciality>(luceneDirectory);

            searcher.AddUpdateIndex(list);
        }

        public IList<string> Search(string name)
        {
            if (!Directory.Exists(luceneDirectory))
                Index();

            var languageSegment = CultureInfo.CurrentCulture;

            var searcher = new MySearcher<Speciality>(luceneDirectory);

            var list = searcher.Search(name).ToList();

            var result = new List<string>();

            foreach (var item in list)
            {
                if(!result.Contains(item.Id))
                    result.Add(item.Id);
            }

            return result;
        }

        private IList<Speciality> SortBasedOnCurrentCulture(IEnumerable<Speciality> list, LCID languageId)
        {
            switch (languageId)
            {
                case LCID.English:
                    return list.OrderBy(x => x.EnglishName).ToList();
                case LCID.Ukrainian:
                    return list.OrderBy(x => x.Name).ToList();
                default:
                    return list.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
