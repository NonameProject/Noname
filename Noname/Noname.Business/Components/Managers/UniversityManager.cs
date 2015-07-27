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
    public class UniversityManager : BaseManager
    {
        IUniversityDataProvider provider;

        protected override string Name
        {
            get
            {
                return "University";
            }

            set
            {
            }
        }

        public UniversityManager(ICacheManager manager, IUniversityDataProvider provider)
            : base(manager)
        {
            this.provider = provider;
        }

        public IList<University> GetList()
        {
            return FromCache<IList<University>>("list", () =>
            {
                var list = provider.GetList();
                var newList = new List<University>();
                foreach (var item in list)
                {
                    newList.Add((University)GetBaseModel(item));
                }
                return newList;
            });
        }
    }
}
