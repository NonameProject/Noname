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
            //univ.City = new Business.Models.City()
            //    {
            //        Universities = new List<University>(),
            //        Name = "Житомир",
            //        NameEN = "Zhytomyr"
            //    };
            //univ.City.Universities.Add(univ);
            //var univ = new Business.Models.University()
            //{
            //    Name = "Національний технічний університет України «Київський політехнічний інститут»",
            //    NameEN = "National Technical University of Ukraine «Kyiv Polytechnic Institute»",
            //    Link = "link",
            //    Rating = 5
            //};
            //provider.Create(univ);
            return View();
        }
    }
}
