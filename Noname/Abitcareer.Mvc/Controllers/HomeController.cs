using Abitcareer.Business.Components;
using System.Web.Mvc;
using System.Collections.Generic;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;

namespace Abitcareer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(short polinom)
        {
            var x = new double[] { 2, 3, 4, 5};
            var y = new double[] { 800, 3000, 5000, 7000 };
            var aproximator = new Approximator(x, y, polinom);
            x = new double[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            return Json(aproximator.CalcY(new List<double>(x)), JsonRequestBehavior.AllowGet);
        }
    }
}
