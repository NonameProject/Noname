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

            Map(x => x.CompressedSalaries).Length(500);

            Map(x => x.CompressedPrices).Length(500);
        }
    }
}
