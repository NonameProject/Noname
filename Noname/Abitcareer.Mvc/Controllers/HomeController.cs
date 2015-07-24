using Abitcareer.NHibernateDataProvider.Data_Providers;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var provider = new NHibernateUniversityDataProvider();
            //var univ = new Business.Models.University()
            //    {
            //        Name = "Житомирський агротехнічний коледж",
            //        NameEN = "Zhitomir Agricultural College",
            //        Link = "link",
            //        Rating = 3
            //    };
            //provider.Create(univ);
            //var list = provider.GetList();
            return View();
        }
    }
}
