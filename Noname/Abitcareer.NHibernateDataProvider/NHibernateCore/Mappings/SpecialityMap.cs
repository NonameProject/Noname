using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class SpecialityMap : BaseMap<Speciality>
    {
        public SpecialityMap()
            : base()
        {
            Map(x => x.Code);

            Map(x => x.DirectionCode);

            //HasManyToMany(x => x.Faculties).Not.LazyLoad().Cascade.All();
        }
    }
}
