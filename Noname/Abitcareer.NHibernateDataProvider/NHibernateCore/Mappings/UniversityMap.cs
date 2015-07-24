using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class UniversityMap : ClassMap<University>
    {
        public UniversityMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name).Not.Nullable().Length(128);

            Map(x => x.NameEN).Not.Nullable().Length(128);

            Map(x => x.Rating).Not.Nullable();

            References(x => x.City).Class<City>();

            Map(x => x.Link).Not.Nullable().Length(128);
        }
    }
}
