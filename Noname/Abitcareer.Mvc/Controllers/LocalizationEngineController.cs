using CultureEngine;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class LocalizationEngineController : Controller
    {
        public ActionResult ChangeCulture(string culture, string routeName)
        {
            if (!CEngine.IsSupported(culture))
            {
                culture = CEngine.DefaultCulture;
            }
            return RedirectToRoute(routeName, new { locale = culture });
        }
    }
}
