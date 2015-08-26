using System;
using System.ComponentModel.DataAnnotations;

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
