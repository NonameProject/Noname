using Abitcareer.Business.Components;
using System.Web.Mvc;
using System.Collections.Generic;
using Abitcareer.Business.Models;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Components.ChartsData;
using System.Linq;
using System;

namespace Abitcareer.Mvc.Controllers
{
    public class HomeController : Controller
    {
        SpecialityManager specialityManager;

        public HomeController(SpecialityManager manager)
        {
            specialityManager = manager;            
        }

        public ActionResult Index()
        {
            var list = from spec in specialityManager.GetList()
                         where (spec.Salaries.Max(x => x.Value) > 0 &&
                            spec.Prices["TopUniversityPrice"] > 0)
                         select spec;
            return View(AutoMapper.Mapper.Map<List<SpecialityViewModel>>(list));
        }

        public ActionResult GetData(string id, short polinom = 3)
        {
            if (String.IsNullOrEmpty(id))
            {
                return Json(false);
            }

            var speciality = specialityManager.GetById(id);

            if (speciality == null)
                return Json(false);

            return Json(new ChartsDataProvider().PrepareData(speciality, polinom));
        }

        public ActionResult GetAdvancedSpeciality(string id)
        {
            try
            {
                var model = AutoMapper.Mapper.Map<SpecialityAdvancedViewModel>(specialityManager.GetById(id));
                if (!String.IsNullOrEmpty(model.EnglishName))
                    model.EnglishName = model.EnglishName.Trim('"');
                return PartialView("Advanced", model);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult GetAdvancedSpeciality(SpecialityAdvancedViewModel model, short polinom = 3)
        {
            var speciality = AutoMapper.Mapper.Map<Speciality>(model);

            if (speciality == null)
                return Json(false);

            specialityManager.PrepareAdvancedData(speciality, model.AdditionalCosts, model.AdditionalIncome);

            return Json(new ChartsDataProvider().PrepareData(speciality, polinom));
        }
    }
}
