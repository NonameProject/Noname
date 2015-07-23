using Abitcareer.NHibernateDataProvider.Data_Providers;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var provider = new NHibernateUniversityDataProvider();
            //provider.Create(new Business.Models.University()
            //    {
            //        Name = "Житомирський Державний Технологічний Унівеситет",
            //        NameEN = "Zhytomyr State Technological University",
            //        Link="link",
            //        CityId = 1,
            //        Rating=5
            //    });
            //var list = provider.GetList();
            return View();
        }
    }
}
