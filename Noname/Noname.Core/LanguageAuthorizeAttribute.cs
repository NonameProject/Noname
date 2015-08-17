using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Abitcareer.Core
{
    public class LanguageAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                string language = null;

                if (HttpContext.Current.Request.UserLanguages.Any())
                {
                    language = HttpContext.Current.Request.UserLanguages[0];
                }
                else
                {
                    language = CultureInfo.DefaultThreadCurrentCulture.ToString();
                }

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                {
                    { "controller", "User" },
                    { "action", "LogIn" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                });
            }
        }
    }
}
