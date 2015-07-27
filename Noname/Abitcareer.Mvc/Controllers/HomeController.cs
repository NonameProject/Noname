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
            return View();
        }
    }
}
