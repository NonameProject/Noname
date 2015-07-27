using Abitcareer.Business.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.NHibernateDataProvider.NHibernateCore.Mappings
{
    public class UniversityMap : BaseMap<University>
    {
        public UniversityMap()
            : base()
        {
            Map(x => x.Rating).Nullable();

            References(x => x.City).Class<City>();

            HasMany(x => x.Faculties).Cascade.SaveUpdate().Inverse();

            Map(x => x.Link).Nullable().Length(128);
        }
    }
}
