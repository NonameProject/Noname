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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Abitcareer.Business.Components.Managers
{
    public class SpecialityManager : BaseManager<Speciality, ISpecialityDataProvider>
    {
        public SpecialityManager(ICacheManager manager, ISpecialityDataProvider provider) : base(manager, provider) {}

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
        }

        public bool TryCreate(Speciality model)
        {
            if (IsSpecialityNameAvailable(model.Name))
            {
                Create(model);
                new MySearcher<Speciality>(luceneDirectory).AddUpdateIndex(model);
                return true;
            }
            return false;
        }

        public void ClearSalaries( )
        {
            provider.ClearSalaries();
        }

        public IList<Speciality> GetList() 
        {
            return FromCache<IList<Speciality>>(Thread.CurrentThread.CurrentUICulture.LCID + "_list", () =>
            {
                var list = SortBasedOnCurrentCulture(provider.GetList(), (LCID)CultureInfo.CurrentCulture.LCID);
                var newList = new List<Speciality>(list.Count);
                foreach (var item in list)
                {
                    newList.Add((Speciality)GetBaseModel(item));
                }
                return newList;
            });            
        }

        public bool TrySave(Speciality editedModel)
        {
            if (String.IsNullOrEmpty(editedModel.EnglishName) || String.IsNullOrEmpty(editedModel.Name))
                return false;

            provider.Update(editedModel);
            new MySearcher<Speciality>(luceneDirectory).AddUpdateIndex(editedModel);
            return true;
        }

        public void Delete(string id)
        {
            ClearCache();
            if(provider.GetById(id) != null)
                provider.Delete(id);
            new MySearcher<Speciality>(luceneDirectory).DeleteFromIndex(id);
        }


       public bool IsSpecialityNameAvailable(string name)
        {
            return provider.GetByName(name) == null;
        }

        public bool IsSpecialityEnglishNameAvailable(string name)
        {
            var translator = new Translation.Translator();
            var value = translator.Translate(name, Translation.Translator.Languages.En, Translation.Translator.Languages.Uk).Replace("\"",String.Empty);
            return provider.GetByName(value) == null;
        }

        public Speciality GetById(string id)
        {
            return provider.GetById(id);
        }

        public void Index()
        {
            var list = GetList();

            var searcher = new MySearcher<Speciality>(luceneDirectory);
            searcher.ClearIndex();
            searcher.AddUpdateIndex(list);
        }

        public IList<string> Search(string name)
        {
            if (!Directory.Exists(luceneDirectory))
                Index();

            var searcher = new MySearcher<Speciality>(luceneDirectory);
            var list = searcher.Search(name, "Name").ToList();
            var result = new List<string>();

            foreach (var item in list)
            {
                if(!result.Contains(item.Id))
                    result.Add(item.Id);
            }
            return result;
        }

        public Speciality GetByName(string name)
        {
            return provider.GetByName(name);
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
