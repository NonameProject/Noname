using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    class FacultiesToSpecialitiesMap : ClassMap<FacultiesToSpecialities>
    {
        public FacultiesToSpecialitiesMap()
        {

            Id(x => x.Id);

            Map(x => x.FacultyId);

            Map(x => x.SpecialityId);
        }
    }
}
