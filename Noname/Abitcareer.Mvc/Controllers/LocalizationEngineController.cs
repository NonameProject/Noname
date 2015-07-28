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

        private string Make(string name)
        {
            return "<uk>_<" + name + ">";
        }

        public ActionResult TestDb()
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

            // ///test///

            //// regionModel.Id = 1;
            //regionModel.Name = "testreg";
            //regionModel.Fields.Add(Make(regionModel.Name), "<qweqweqeqeq>");

            ////cityModel.Id = 1;
            //cityModel.Name = "testcity";
            //cityModel.Region = regionModel;
            //cityModel.Fields.Add(Make(cityModel.Name), "<12313>");

            //universityModel.City = cityModel;
            ////universityModel.Id = 1;
            //universityModel.Name = "testuniver";
            //universityModel.Fields.Add(Make(universityModel.Name), "<12313123>");


            ////facultyModel.Id = 1;
            //facultyModel.Name = "testfac";
            //facultyModel.Fields.Add(Make(facultyModel.Name), "<123123123>");


            ////specialityModel.Id = 1;
            //specialityModel.Name = "testspec";
            //specialityModel.Fields.Add(specialityModel.Name, "<123123>");

            //specialityModel.Faculties.Add(facultyModel);

            //facultyModel.Specialities.Add(specialityModel);
            //facultyModel.University = universityModel;


            ////universityModel.Faculties.Add(facultyModel);
            //universityModel.City = cityModel;

            //cityModel.Universities.Add(universityModel);

            //regionModel.Cities.Add(cityModel);


            ////specialityManager.Create(specialityModel);
            //regionManager.Create(regionModel);
            //cityManager.Create(cityModel);
            //universityManager.Create(universityModel);
            //facultyManager.CreateFacultyToSpeciality(facultyModel, specialityModel);

            ///test///


            foreach (XElement region in doc.Root.Elements())
            {
                if (!regList.Contains(region.Attribute("name").Value))
                {
                    regionModel.Name = region.Attribute("name").Value;
                    regionModel.Id = Guid.NewGuid().ToString();
                    regionModel.Fields.Add(Make(regionModel.Name), translator.Translate(regionModel.Name, Translator.Languages.Uk, Translator.Languages.En));
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
                        cityModel.Fields.Add(Make(cityModel.Name), translator.Translate(cityModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                        cityList.Add(cityModel.Name);
                        cityManager.Create(cityModel);
                    }
 

                    foreach (XElement university in city.Elements())
                    {
                        if (!univerList.Contains(university.Attribute("name").Value))
                        {
                            universityModel.Name = university.Attribute("name").Value;
                            universityModel.Id = Guid.NewGuid().ToString();
                            universityModel.City = cityModel;
                            universityModel.Fields.Add(Make(universityModel.Name), translator.Translate(universityModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                            univerList.Add(universityModel.Name);
                            universityManager.Create(universityModel);
                        }

                        universityModel.Fields.Clear();
                        cityModel.Fields.Clear();
                        regionModel.Fields.Clear();


                        //foreach (XElement faculty in university.Elements())
                        //{
                        //    facultyModel.Id = Convert.ToInt32(faculty.Attribute("id").Value);
                        //    facultyModel.Name = faculty.Attribute("name").Value;

                        //    foreach (XElement speciality in faculty.Elements())
                        //    {
                        //        specialityModel.Id = Convert.ToInt32(speciality.Attribute("id").Value);
                        //        specialityModel.Name = speciality.Attribute("name").Value;
                        //    }
                        //}
                    }
                }
            }

            var list = AutoMapper.Mapper.Map<List<UniversityViewModel>>(universityManager.GetList());
            return View(list);
        }

        public ActionResult Import()
        {
            return View();
        }
    }
}
