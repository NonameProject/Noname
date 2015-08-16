using Abitcareer.Business.Components.Translation;
using Abitcareer.Business.Components.XmlServices.Entities;
using Abitcareer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Abitcareer.Business.Components.XmlServices
{
    public class DataImportXmlService
    {
        private Translator translator = null;

        private XDocument document = null;

        private string filePath = null;

        private readonly int LCID_EN = 1033;

        public DataImportXmlService(string filePath)
        {
            translator = new Translator();

            this.filePath = filePath;
        }

        private List<string> regionList = new List<string>();

        private List<string> cityList = new List<string>();

        private List<string> universityList = new List<string>();

        private List<string> facultyList = new List<string>();

        private List<string> specialityList = new List<string>();

        public List<NodeModel> Parse()
        {
            if (!OpenDataFile(filePath))
                return null;

            var results = new List<NodeModel>();           

            foreach(XElement region in document.Root.Elements())
            {
                if (String.IsNullOrEmpty(region.Attribute("name").Value) || regionList.Contains(region.Attribute("name").Value))
                    continue;

                var regionModel = new Region();

                regionList.Add(region.Attribute("name").Value);

                regionModel.Name = region.Attribute("name").Value;

                regionModel.Fields.Add(GetKey(), translator.Translate(regionModel.Name, Translator.Languages.Uk, Translator.Languages.En));
                
                foreach(XElement city in region.Elements())
                {
                    if (String.IsNullOrEmpty(city.Attribute("name").Value) || cityList.Contains(city.Attribute("name").Value))
                        continue;

                    var cityModel = new City();

                    cityList.Add(city.Attribute("name").Value);

                    cityModel.Name = city.Attribute("name").Value;

                    cityModel.Region = regionModel;

                    cityModel.Fields.Add(GetKey(), translator.Translate(cityModel.Name, Translator.Languages.Uk, Translator.Languages.En));

                    foreach (XElement university in city.Elements())
                    {
                        if (String.IsNullOrEmpty(university.Attribute("name").Value) || universityList.Contains(university.Attribute("name").Value))
                            continue;

                        var universityModel = new University();

                        universityList.Add(university.Attribute("name").Value);

                        universityModel.Name = university.Attribute("name").Value;

                        universityModel.City = cityModel;

                        universityModel.Fields.Add(GetKey(), translator.Translate(universityModel.Name, Translator.Languages.Uk, Translator.Languages.En));

                        foreach (XElement faculty in university.Elements())
                        {
                            if (String.IsNullOrEmpty(faculty.Attribute("name").Value))
                                continue;

                            var facultyModel = new Faculty();

                            facultyModel.University = universityModel;

                            facultyModel.Name = faculty.Attribute("name").Value;

                            facultyModel.Fields.Add(GetKey(), translator.Translate(facultyModel.Name, Translator.Languages.Uk, Translator.Languages.En));

                            foreach(XElement speciality in faculty.Elements())
                            {

                                if (String.IsNullOrEmpty(speciality.Attribute("name").Value))
                                    continue;

                                var specialityModel = new Speciality();

                                var facultyToSpecialityModel = new FacultyToSpeciality();

                                specialityModel.Name = speciality.Attribute("name").Value;

                                specialityModel.Fields.Add(GetKey(), translator.Translate(specialityModel.Name, Translator.Languages.Uk, Translator.Languages.En));

                                facultyToSpecialityModel.Speciality = specialityModel;

                                facultyToSpecialityModel.Faculty = facultyModel;

                                var node = new NodeModel
                                {
                                    Region = regionModel,
                                    City = cityModel,
                                    University = universityModel,
                                    Faculty = facultyModel,
                                    Speciality = specialityModel,
                                    FacultyToSpeciality = facultyToSpecialityModel
                                };

                                results.Add(node);
                            }
                        }
                    }
                }
            }

            return results;
        }

        private bool OpenDataFile(string filePath)
        {
            try
            {
                document = XDocument.Load(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetKey()
        {
            return string.Format("<{0}>_<{1}>", LCID_EN, "Name");
        }

    }
}
