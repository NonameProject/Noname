using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Interfaces;
using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abitcareer.Business.Components.Managers
{
    public class UniversityManager : BaseManager<University, IUniversityDataProvider>
    {
        public UniversityManager(ICacheManager manager, IUniversityDataProvider provider) : base(manager, provider) { }

        protected override string Name
        {
            get
            {
                return "University";
            }
        }

        public IList<University> GetList()
        {
            return FromCache<IList<University>>(Thread.CurrentThread.CurrentUICulture.LCID + "_list", () =>
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
