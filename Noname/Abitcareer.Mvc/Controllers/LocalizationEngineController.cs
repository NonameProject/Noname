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
        UniversityManager universityManager;
        RegionManager regionManager;
        CityManager cityManager;
        FacultyManager facultyManager;
        SpecialityManager specialityManager;
        Translator translator = new Translator();

        public LocalizationEngineController(UniversityManager universityManager, RegionManager regionManager,
            SpecialityManager specialityManager, CityManager cityManager, FacultyManager facultyManager)
        {
            this.universityManager = universityManager;
            this.regionManager = regionManager;
            this.facultyManager = facultyManager;
            this.specialityManager = specialityManager;
            this.cityManager = cityManager;
        }

        public ActionResult ChangeCulture(string culture, string routeName)
        {
            return RedirectToRoute(routeName, new { locale = culture });
        }

        public ActionResult TestDb()
        {

            var list = AutoMapper.Mapper.Map<List<UniversityViewModel>>(universityManager.GetList());
            return View(list);
        }
    }
}
