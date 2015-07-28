using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class FacultyMap : BaseMap<Faculty>
    {
        public FacultyMap()
            : base()
        {
            References(x => x.University).Class<University>();

            HasManyToMany(x => x.Specialities).Not.LazyLoad().Cascade.All();
        }
    }
}
