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
    public class FacultyManager : BaseManager<Faculty, IFacultyDataProvider>
    {
        public FacultyManager(ICacheManager manager, IFacultyDataProvider provider) : base(manager, provider) { }

        protected override string Name
        {
            get
            {
                return "Faculty";
            }
        }

        public Faculty GetByName(string name)
        {
            return provider.GetByName(name);
        }
    }
}
