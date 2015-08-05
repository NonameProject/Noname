using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class SpecialityViewModel : BaseViewModel
    {
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
