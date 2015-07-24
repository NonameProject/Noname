using Abitcareer.Business.Models;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using System.Collections.Generic;
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
            //        Name = "житомирський агротехнічний коледж",
            //        NameEN = "Zhytomyr Agricultural College",
            //        Link = "link",
            //        Rating = 5
            //    };
            //univ.City = new Business.Models.City()
            //    {
            //        Universities = new List<University>(),
            //        Name = "Житомир",
            //        NameEN = "Zhytomyr"
            //    };
            //univ.City.Universities.Add(univ);
            //provider.Create(univ);
            //var list = provider.GetList();
            return View();
        }
    }
}
