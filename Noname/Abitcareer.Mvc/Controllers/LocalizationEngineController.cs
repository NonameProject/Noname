using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class LocalizationEngineController : Controller
    {
        public ActionResult ChangeCulture(string culture, string routeName)
        {
            return RedirectToRoute(routeName, new { locale = culture });
        }
    }
}
