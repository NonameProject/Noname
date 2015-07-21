using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class TestLocalizationController : Controller
    {
        //
        // GET: /TestLocalization/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            var url = HttpUtility.UrlDecode(returnUrl);
            url = url.Substring(6);
            url = string.Concat("/", lang, url);
            return Redirect(url);
        }

    }
}
