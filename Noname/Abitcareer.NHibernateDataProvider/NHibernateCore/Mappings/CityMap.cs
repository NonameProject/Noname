using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class CityMap : BaseMap<City>
    {
        public CityMap()
            : base()
        {
            HasMany(x => x.Universities).Inverse();

            References(x => x.Region).Class<Region>();
        }
    }
}
