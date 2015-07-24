using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name).Not.Nullable().Length(128);

            Map(x => x.NameEN).Not.Nullable().Length(128);

            HasOne(x => x.University).Cascade.All();

            References(x => x.Region).Unique();
        }
    }
}
