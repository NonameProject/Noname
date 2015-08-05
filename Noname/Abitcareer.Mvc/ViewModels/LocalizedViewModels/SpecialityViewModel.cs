using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class SpecialityViewModel : BaseViewModel
    {
        [ScaffoldColumn(false)]
        public virtual int DirectionCode { get; set; }

        [ScaffoldColumn(false)]
        public virtual int Code { get; set; }

        public Dictionary<int, int> Salaries = new Dictionary<int, int>();
        public SpecialityViewModel()
        {
            Salaries[1] = 0;
            Salaries[2] = 0;
            Salaries[3] = 0;
            Salaries[4] = 0;
            Salaries[5] = 0;
            Salaries[10] = 0;
            Salaries[20] = 0;
            Name = "Default";
        }
    }
}
