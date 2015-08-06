using Abitcareer.Business.Components.Managers;
using Abitcareer.Business.Data_Providers_Contracts;
using Abitcareer.Business.Models;
using Abitcareer.Core.CustomExceptions;
using Elmah;
using System.Web.Mvc;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using Abitcareer.Business.Components.Translation;
using System.Threading;

namespace Abitcareer.Mvc.Controllers
{
    public class DataImportController : Controller
    {
        FacultyManager facultyManager;

        SpecialityManager specialityManager;

        IFacultiesToSpecialitiesDataProvider facToSpecProvider;

        UniversityManager universityManager;

        CityManager cityManager;

        RegionManager regionManager;

        public DataImportController(CityManager cityManager ,RegionManager regionManager,UniversityManager universityManager,FacultyManager facultyManager, SpecialityManager specialityManager,
            IFacultiesToSpecialitiesDataProvider facToSpecProvider)
        {
            this.facultyManager = facultyManager;

            this.specialityManager = specialityManager;

            this.facToSpecProvider = facToSpecProvider;

            this.regionManager = regionManager;

            this.cityManager = cityManager;

            this.universityManager = universityManager;
        }

        private string GetKey()
        {
            return string.Format("<{0}>_<{1}>", Thread.CurrentThread.CurrentUICulture.LCID, "Name");
        }

        public void  Import()
        {
            Translator translator = new Translator();

            var doc = XDocument.Load(Server.MapPath("~/Data.xml"));

            var regionModel = new Region();

            var cityModel = new City();

            var universityModel = new University();

            var facultyModel = new Faculty();

            var specialityModel = new Speciality();

            var facToSpec = new FacultiesToSpecialities();

            var regList = new List<string>();

            var cityList = new List<string>();

            var univerList = new List<string>();

            var facultyList = new List<string>();

            var specialityList = new List<string>();

            foreach (XElement region in doc.Root.Elements())
            {
                if (!regList.Contains(region.Attribute("name").Value))
                {
                    regionModel = new Region();
                    regionModel.Name = region.Attribute("name").Value;
                    regionModel.Fields.Add(GetKey(), translator.Translate(regionModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                    regList.Add(regionModel.Name);
                    regionManager.Create(regionModel);
                }

                foreach (XElement city in region.Elements())
                {
                    if (!cityList.Contains(city.Attribute("name").Value))
                    {
                        cityModel = new City();
                        cityModel.Name = city.Attribute("name").Value;
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
                            universityModel = new University();
                            universityModel.Name = university.Attribute("name").Value;
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
                                facultyModel = new Faculty();
                                facultyModel.Name = faculty.Attribute("name").Value;
                                facultyModel.Fields.Add(GetKey(), translator.Translate(facultyModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                                facultyList.Add(facultyModel.Name);
                                facultyManager.Create(facultyModel);
                                facultyModel.Fields.Clear();
                            }

                            foreach (XElement speciality in faculty.Elements())
                            {
                                if (!specialityList.Contains(speciality.Attribute("name").Value))
                                {
                                    specialityModel = new Speciality();
                                    facToSpec = new FacultiesToSpecialities();
                                    specialityModel.Name = speciality.Attribute("name").Value;
                                    specialityModel.Fields.Add(GetKey(), translator.Translate(specialityModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                                    specialityList.Add(specialityModel.Name);
                                    facToSpec.SpecialityId = specialityModel.Id;
                                    facToSpec.FacultyId = facultyModel.Id;
                                    specialityManager.Create(specialityModel);
                                    facToSpecProvider.Create(facToSpec);
                                    specialityModel.Fields.Clear();
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}