using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class SpecialityMap : ClassMap<Speciality>
    {
        public SpecialityMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name).Not.Nullable().Length(128);

            Map(x => x.NameEN).Not.Nullable().Length(128);

            Map(x => x.Code);

            Map(x => x.DirectionCode);

            HasManyToMany(x => x.Faculties).Not.LazyLoad().Cascade.All();
        }
    }
}
