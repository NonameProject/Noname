using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    class FacultyToSpecialityMap : ClassMap<FacultyToSpeciality>
    {
        public FacultyToSpecialityMap()
        {

            Id(x => x.Id);

            References(x => x.Faculty);

            References(x => x.Speciality);

            Map(x => x.Price);
        }
    }
}
