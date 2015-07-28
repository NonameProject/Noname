using System.Collections.Generic;
namespace Abitcareer.Mvc.ViewModels.LocalizedViewModels
{
    public class CityViewModel : BaseViewModel
    {
        public int Rating { get; set; }

        public string Link { get; set; }

        public IList<UniversityViewModel> Universities { get; set; }
    }
}
