using Elmah;
using Abitcareer.Mvc.Components.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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