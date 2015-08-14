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
using System.Linq;
using Abitcareer.Core;
using Abitcareer.Business.Components.Lucene;
using System.IO;

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

        public ActionResult Specialities()
        {
            var spec = specialityManager.GetList() as List<Speciality>;
            var tmp = spec.Where(x =>
                !string.IsNullOrEmpty(x.Name) && !string.IsNullOrEmpty(x.Id)).ToList();
            var result = AutoMapper.Mapper.Map<List<SpecialityViewModel>>(tmp);
            return View(result);
        }

        public ActionResult EditSpecialities(string id)
        {
            try
            {
                var model = AutoMapper.Mapper.Map<SpecialityViewModel>(specialityManager.GetById(id));
                if (!String.IsNullOrEmpty(model.EnglishName)) model.EnglishName = model.EnglishName.Trim('"');
                return PartialView("EditSpeciality", model);
            }
            catch(Exception e)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult AddSpeciality(SpecialityViewModel viewModel)
        {
            var model = AutoMapper.Mapper.Map<Speciality>(viewModel);
            if (!specialityManager.IsExists(model))
            {
                specialityManager.Create(model);
                return PartialView("SpecialityPartial", viewModel);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult Save(SpecialityViewModel editedModel)
        {
            var mappedModel = AutoMapper.Mapper.Map<Speciality>(editedModel);
            var result = specialityManager.TrySave(mappedModel);
            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteSpeciality(string id)
        {
            specialityManager.Delete(id);
            return Json(true);
        }

        [HttpGet]
        public ActionResult AddSpeciality()
        {
            return PartialView(new SpecialityViewModel());
        }

        public ActionResult EditSpeciality()
        {
            return PartialView();
        }

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
