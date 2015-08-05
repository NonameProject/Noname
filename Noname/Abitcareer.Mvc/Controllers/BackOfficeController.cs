using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using CultureEngine;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;
using Abitcareer.Business.Components.Translation;
using System.Threading;


namespace Abitcareer.Web.Components
{
    public class BackOfficeController : Controller
    {
        //
        // GET: /BackOfiice/

        public ActionResult AllSpecialities()
        {        
            return View();
        }

        public ActionResult EditSpecialities()
        {
            return View();
        }
    }
}
