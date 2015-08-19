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
    public class RegionManager : BaseManager<Region, IRegionDataProvider>
    {
        public RegionManager(ICacheManager manager, IRegionDataProvider provider) : base(manager, provider) { }

        protected override string Name
        {
            get
            {
                return "Region";
            }
        }
    }
}
