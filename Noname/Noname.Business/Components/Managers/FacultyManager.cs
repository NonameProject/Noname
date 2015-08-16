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
    public class FacultyManager : BaseManager
    {
        IFacultyDataProvider provider;

        protected override string Name
        {
            get
            {
                return "Faculty";
            }

            set
            {
            }
        }

        public FacultyManager(ICacheManager manager, IFacultyDataProvider provider)
            : base(manager)
        {
            this.provider = provider;
        }

        public void Create(Faculty model)
        {
            ClearCache();
            provider.Create(model);
        }

        public Faculty GetByName(string name)
        {
            return provider.GetByName(name);
        }
    }
}
