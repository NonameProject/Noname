using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using CultureEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;
using Abitcareer.Business.Components.Translation;
using System.Threading;

namespace Abitcareer.Mvc.Controllers
{
    public class LocalizationEngineController : Controller
    {
        public ActionResult ChangeCulture(string culture, string routeName, string anchor = "")
        {
            if(string.IsNullOrEmpty(anchor))
            {
                return RedirectToRoute(routeName, new { locale = culture });
            }
            return Redirect(Url.RouteUrl(routeName, new { locale = culture }) + "#" + anchor);            
        }
    }
}
