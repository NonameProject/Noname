using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.NHibernateDataProvider.Data_Providers;
using CultureEngine;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Controllers
{
    public class LocalizationEngineController : Controller
    {
        UniversityManager manager;

        public LocalizationEngineController(UniversityManager manager)
        {
            this.manager = manager;
        }

        public ActionResult ChangeCulture(string culture, string routeName)
        {
            return RedirectToRoute(routeName, new { locale = culture });
        }

        public ActionResult TestDb()
        {
            var list = AutoMapper.Mapper.Map<List<UniversityViewModel>>(manager.GetList());
            return View(list);
        }
    }
}
