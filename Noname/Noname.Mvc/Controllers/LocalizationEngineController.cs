using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class LocalizationEngineController : Controller
    {
        //
        // GET: /CultureEngine/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeLocalization(string newLocalization, string returnUrl)
        {
            CultureEngine.LEngine.SetCulture(newLocalization, Request.RequestContext);
            return Redirect(HttpUtility.UrlDecode(returnUrl));
        }
    }
}
