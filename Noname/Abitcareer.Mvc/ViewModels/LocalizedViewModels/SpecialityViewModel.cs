﻿using System;
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

        public int StartOfWorking { get; set; }

        public string ImageLink { get; set; }

        public SpecialityViewModel()
        {
            StartOfWorking = 1;

            Salaries = new Dictionary<int, int>();

            Prices = new Dictionary<string, int>();

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

            Id = Guid.NewGuid().ToString();
        }
    }
}
