using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class SpecialityViewModel : BaseViewModel
    {
        [Remote("IsSpecialityNameAvailable", "BackOffice", ErrorMessage = " ", HttpMethod="POST")]
        public override string Name { get; set; }

        public string EnglishName { get; set; }
        [ScaffoldColumn(false)]
        public virtual int DirectionCode { get; set; }

        [ScaffoldColumn(false)]
        public virtual int Code { get; set; }

        public Dictionary<int, int> Salaries { get; set; }

        public  Dictionary<string, int> Prices { get; set; }

        public int TopPrice
        {
            get
            {
                int x;
                Prices.TryGetValue("TopUniversityPrice", out x);
                return x;
            } set{
                Prices["TopUniversityPrice"] = value;
            }
        }

        public int MiddlePrice
        {
            get
            {
                int x;
                Prices.TryGetValue("MiddleUniversityPrice", out x);
                return x;
            }
            set
            {
                Prices["MiddleUniversityPrice"] = value;
            }
        }

        public int LowPrice
        {
            get
            {
                int x;
                Prices.TryGetValue("LowUniversityPrice", out x);
                return x;
            }
            set
            {
                Prices["LowUniversityPrice"] = value;
            }
        }

        public int StartOfWorking { get; set; }

        public string ImageLink { get; set; }

        public SpecialityViewModel()
        {
            StartOfWorking = 1;

            Salaries = new Dictionary<int, int>();

            Prices = new Dictionary<string, int>();

            Salaries[1] = -1;
            Salaries[2] = -1;
            Salaries[3] = -1;
            Salaries[4] = -1;
            Salaries[5] = -1;
            Salaries[10] = -1;
            Salaries[20] = -1;

            TopPrice = -1;
            MiddlePrice = -1;
            LowPrice = -1;

            Id = Guid.NewGuid().ToString();
        }
    }
}
