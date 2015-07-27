using Abitcareer.Core.CustomExceptions;
using Elmah;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
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