using Abitcareer.Core.CustomExceptions;
using CultureEngine;
using Elmah;
using System.Web.Mvc;
using System.Web.Routing;

namespace Abitcareer.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("NotFound");
        }

        public ActionResult NotFound( )
        {
            string userCulture = HttpContext.Request.Cookies["lang"].Value ?? CEngine.Instance.GetCultureByUserLanguages(HttpContext.Request.UserLanguages),
                path = HttpContext.Request.Path;
            RouteData routeData = RouteTable.Routes.GetRouteData(HttpContext);
            var hasCultureSegment = System.Text.RegularExpressions.Regex.IsMatch(path, "[A-z]{2}-[A-z]{2}");
            if (!hasCultureSegment)
            {
                CEngine.Instance.SetCultureForThread(userCulture);
                return Redirect("~/" + userCulture + path);
            }
            return View();
        }

        public void LogJavaScriptError(string message)
        {
            ErrorSignal
                .FromCurrentContext()
                    .Raise(new JavaScriptException(message));
        }
    }
}