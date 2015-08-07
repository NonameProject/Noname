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

        public ActionResult GetData(short polinom = 3)
        {
            var x = new double[] { 1, 2, 3, 4, 5};
            var y = new double[] { 0, 1500, 2800, 4000, 5200 };
            var aproximator = new Approximator(x, y, polinom);
            //x = new double[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var list = Point.GetZeroList(2);
            list.AddRange(aproximator.CalcY(2, 15));
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
