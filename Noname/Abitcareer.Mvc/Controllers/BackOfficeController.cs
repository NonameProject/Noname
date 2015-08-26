using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Models;
using Abitcareer.Core;
using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Abitcareer.Web.Components
{
    [LanguageAuthorize]
    public class BackOfficeController : Controller
    {
        SpecialityManager specialityManager;

        public BackOfficeController(SpecialityManager manager)
        {
            specialityManager = manager;
        }

        [AllowAnonymous]
        public ActionResult Specialities()
        {
            var specialities = (IEnumerable<Speciality>)specialityManager.GetList();
            if (!User.Identity.IsAuthenticated)
            {
                specialities = from spec in specialities
                               where (spec.Salaries.Max(x => x.Value) > 0 &&
                                  spec.TopPrice > 0)
                               select spec;
            }
            var tmp = specialities.Where(x =>
                !String.IsNullOrEmpty(x.Name) && !String.IsNullOrEmpty(x.Id)).ToList();
            var result = AutoMapper.Mapper.Map<List<SpecialityViewModel>>(tmp);
            return View(result);
        }

        [HttpGet]
        public ActionResult EditSpeciality(string id)
        {
            try
            {
                var model = AutoMapper.Mapper.Map<SpecialityViewModel>(specialityManager.GetById(id));
                if (!String.IsNullOrEmpty(model.EnglishName)) 
                    model.EnglishName = model.EnglishName.Trim('"');
                specialityManager.Index();
                return PartialView(model);
            }
            catch(Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult AddSpeciality(SpecialityViewModel viewModel)
        {
            var model = AutoMapper.Mapper.Map<Speciality>(viewModel);  
       
            if (specialityManager.TryCreate(model))
            {
                var localizedViewModel = AutoMapper.Mapper.Map<SpecialityViewModel>((Speciality)specialityManager.GetBaseModel(model));
                return PartialView("SpecialityPartial", localizedViewModel);
            }         
      
            return Json(false);
        }

        [HttpPost]
        public JsonResult IsSpecialityNameAvailable(string name)
        {
            return Json(specialityManager.IsSpecialityNameAvailable(name));
        }

        [HttpPost]
        public ActionResult EditSpeciality(SpecialityViewModel editedModel)
        {
            var mappedModel = AutoMapper.Mapper.Map<Speciality>(editedModel);
            var result = specialityManager.TrySave(mappedModel);
            var localizedViewModel = AutoMapper.Mapper.Map<SpecialityViewModel>((Speciality)specialityManager.GetBaseModel(mappedModel));
            return PartialView("SpecialityPartial", localizedViewModel);
        }

        [HttpPost]
        public ActionResult DeleteSpeciality(string id)
        {
            var model = specialityManager.GetById(id);
            specialityManager.Delete(id);
            return Json(model.Name);
        }

        [HttpGet]
        public ActionResult AddSpeciality()
        {
            return PartialView(new SpecialityViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SearchForSpeaciality(string name)
        {
            var list = specialityManager.Search(name);
            return Json(list);
        }

        public ActionResult IndexSpecialities()
        {
            var list = specialityManager.GetList();
            specialityManager.Index();
            return RedirectToRoute("specialities");
        }
    }
}
