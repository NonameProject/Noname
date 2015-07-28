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

        private string GetKey()
        {
            return string.Format("<{0}>_<{1}>", Thread.CurrentThread.CurrentUICulture.LCID, "Name");
        }

        public ActionResult TestDb()
        {

            var list = AutoMapper.Mapper.Map<List<UniversityViewModel>>(universityManager.GetList());
            return View(list);
        }

        public ActionResult Import()
        {
            var doc = XDocument.Load(Server.MapPath("~/Data.xml"));

            var regionModel = new Region();

            var cityModel = new City();

            var universityModel = new University();

            var facultyModel = new Faculty();

            var specialityModel = new Speciality();

            var regList = new List<string>();

            var cityList = new List<string>();

            var univerList = new List<string>();

            var facultyList = new List<string>();

            var specialityList = new List<string>();


            foreach (XElement region in doc.Root.Elements())
            {
                if (!regList.Contains(region.Attribute("name").Value))
                {
                    regionModel.Name = region.Attribute("name").Value;
                    regionModel.Id = Guid.NewGuid().ToString();
                    regionModel.Fields.Add(GetKey(), translator.Translate(regionModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                    regList.Add(regionModel.Name);
                    regionManager.Create(regionModel);

                }

                foreach (XElement city in region.Elements())
                {
                    if (!cityList.Contains(city.Attribute("name").Value))
                    {
                        cityModel.Name = city.Attribute("name").Value;
                        cityModel.Id = Guid.NewGuid().ToString();
                        cityModel.Region = regionModel;
                        cityModel.Fields.Add(GetKey(), translator.Translate(cityModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                        cityList.Add(cityModel.Name);
                        cityManager.Create(cityModel);
                        cityModel.Fields.Clear();

                    }


                    foreach (XElement university in city.Elements())
                    {
                        if (!univerList.Contains(university.Attribute("name").Value))
                        {
                            universityModel.Name = university.Attribute("name").Value;
                            universityModel.Id = Guid.NewGuid().ToString();
                            universityModel.City = cityModel;
                            universityModel.Fields.Add(GetKey(), translator.Translate(universityModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                            univerList.Add(universityModel.Name);
                            universityManager.Create(universityModel);
                            universityModel.Fields.Clear();

                        }


                        foreach (XElement faculty in university.Elements())
                        {
                            if (!facultyList.Contains(faculty.Attribute("name").Value))
                            {
                                facultyModel.Name = faculty.Attribute("name").Value;
                                facultyModel.Id = Guid.NewGuid().ToString();
                                facultyModel.Fields.Add(GetKey(), translator.Translate(facultyModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                                facultyList.Add(facultyModel.Name);
                                facultyManager.Create(facultyModel);
                                facultyModel.Fields.Clear();
                            }

                            foreach (XElement speciality in faculty.Elements())
                            {
                                if (!specialityList.Contains(speciality.Attribute("name").Value))
                                {
                                    specialityModel.Name = faculty.Attribute("name").Value;
                                    specialityModel.Id = Guid.NewGuid().ToString();
                                    specialityModel.Fields.Add(GetKey(), translator.Translate(specialityModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                                    specialityList.Add(specialityModel.Name);
                                    specialityManager.Create(specialityModel);
                                    specialityModel.Fields.Clear();
                                }
                            }
                        }
                    }
                }
            }
            return View("TestDb");
        }
    }
}
