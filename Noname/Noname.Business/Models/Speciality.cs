using System.Collections.Generic;
using Abitcareer.Business.Components.Extensions;
using System;

namespace Abitcareer.Business.Models
{
    public class Speciality : BaseModel
    {
        public virtual int DirectionCode { get; set; }

        public virtual int Code { get; set; }

        public virtual Dictionary<int, int> Salaries { get; set; }

        public virtual Dictionary<string, Decimal> Prices { get; set; }

        public virtual int StartOfWorking { get; set; }

        public Speciality()
        {
            StartOfWorking = 1;

            Salaries = new Dictionary<int, int>();

            Prices = new Dictionary<string, Decimal>();

            Salaries[1] = 0;
            Salaries[2] = 0;
            Salaries[3] = 0;
            Salaries[4] = 0;
            Salaries[5] = 0;
            Salaries[10] = 0;
            Salaries[20] = 0;

            Prices["TopUniversityPrice"] = 0;
            Prices["MiddleUniversityPrice"] = 0;
            Prices["LowUniversityPrice"] = 0;
        }

        public virtual string CompressedSalaries
        {
            get
            {
                return Salaries.ToXmlString<int, int>();
            }
            set
            {
                Salaries = value.ToDictionary<int, int>();
            }
        }

        public virtual string CompressedPrices
        {
            get
            {
                return Prices.ToXmlString<string, Decimal>();
            }
            set
            {
                Prices = value.ToDictionary<string, Decimal>();
            }
        }

        public virtual string EnglishName
        {
            get
            {
                if (!Fields.ContainsKey("<1033>_<Name>"))
                    Fields.Add("<1033>_<Name>", null);
                return Fields["<1033>_<Name>"];
            }
            set
            {
                if (!Fields.ContainsKey("<1033>_<Name>"))
                    Fields.Add("<1033>_<Name>", null);
                Fields["<1033>_<Name>"] = value;
            }
        }

    }
}
