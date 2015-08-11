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
            var model = AutoMapper.Mapper.Map<SpecialityViewModel>(specialityManager.GetById(id));
            if (!String.IsNullOrEmpty(model.EnglishName)) model.EnglishName = model.EnglishName.Trim('"');
            return PartialView("EditSpeciality",model);
        }

        //[HttpPost]
        //public ActionResult AddSpeciality(SpecialityViewModel viewModel)
        //{
        //    var model = AutoMapper.Mapper.Map<Speciality>(viewModel);
        //    return Json(specialityManager.Create(model));
        //}

        [HttpPost]
        public ActionResult Save(SpecialityViewModel editedModel)
        {
            var mappedModel = AutoMapper.Mapper.Map<Speciality>(editedModel);
            var result = specialityManager.TrySave(mappedModel);
            return Json(result);
        }

        [HttpPost]
        public ActionResult AddSpeciality()
        {
            return PartialView(new SpecialityViewModel());
        }

        public ActionResult EditSpeciality()
        {
            return PartialView();
        }
    }
}
