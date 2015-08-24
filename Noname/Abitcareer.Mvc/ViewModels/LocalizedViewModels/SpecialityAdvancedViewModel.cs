using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class SpecialityAdvancedViewModel : SpecialityViewModel
    {
        [Range(0, Int32.MaxValue, ErrorMessage = "value must be > 0")]
        public int AdditionalCosts { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "value must be > 0")]
        public int AdditionalIncome { get; set; }

        public SpecialityAdvancedViewModel() : base()
        {
            AdditionalCosts = 0;
            AdditionalIncome = 0;
        }
    }
}
