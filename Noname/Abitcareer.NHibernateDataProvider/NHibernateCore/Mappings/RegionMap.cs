using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class RegionMap : ClassMap<Region>
    {
        public RegionMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name).Not.Nullable().Length(128);

            Map(x => x.NameEN).Not.Nullable().Length(128);

            HasMany(x => x.Cities).Inverse();
        }
    }
}
