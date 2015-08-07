using System.Collections.Generic;
using Abitcareer.Business.Components.Extensions;

namespace Abitcareer.Business.Models
{
    public class Speciality : BaseModel
    {
        public virtual int DirectionCode { get; set; }

        public virtual int Code { get; set; }

        public virtual Dictionary<int, int> Salaries { get; set; }

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

        public virtual string EnglishName
        {
            get
            {
                if (!Fields.ContainsKey("<1058>_<Name>"))
                    Fields.Add("<1058>_<Name>", null);
                return Fields["<1058>_<Name>"];
            }
            set
            {
                if (!Fields.ContainsKey("<1058>_<Name>"))
                    Fields.Add("<1058>_<Name>", null);
                Fields["<1058>_<Name>"] = value;
            }
        }
    }
}
