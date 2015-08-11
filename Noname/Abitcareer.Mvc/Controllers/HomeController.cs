using Abitcareer.Business.Components;
using System.Web.Mvc;
using System.Collections.Generic;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Components.ChartsData;

namespace Abitcareer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        SpecialityManager specialityManager;

        public HomeController(SpecialityManager manager)
        {
            specialityManager = manager;            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(string id, short polinom = 3)
        {
            if (string.IsNullOrEmpty(id))
                return Json(false);
            var speciality = specialityManager.GetById(id);
            if (speciality == null)
                return Json(false);

            return Json(new ChartsDataProvider().PrepareData(speciality, polinom), JsonRequestBehavior.AllowGet);
        }
    }
}
