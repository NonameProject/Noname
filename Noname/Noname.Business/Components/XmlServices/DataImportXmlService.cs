using Abitcareer.Business.Components.Translation;
using Abitcareer.Business.Components.XmlServices.Entities;
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

        private int LCID_EN = 1033;

        public DataImportXmlService(string filePath)
        {
            translator = new Translator();

            this.filePath = filePath;
        }

        public List<NodeModel> Parse()
        {
            if (!OpenDataFile(filePath))
                return null;

            var results = new List<NodeModel>();

            foreach(XElement region in document.Root.Elements())
            {
                var node = new NodeModel();

                node.Region.Name = region.Attribute("name").Value;
                
                node.Region.Fields.Add(GetKey(), translator.Translate(node.Region.Name, Translator.Languages.Uk, Translator.Languages.En));
                
                foreach(XElement city in region.Elements())
                {
                    node.City.Name = city.Attribute("name").Value;
                    
                    node.City.Region = node.Region;
                    
                    node.City.Fields.Add(GetKey(), translator.Translate(node.City.Name, Translator.Languages.Uk, Translator.Languages.En));

                    foreach (XElement university in city.Elements())
                    {
                        node.University.Name = university.Attribute("name").Value;
                        
                        node.University.City = node.City;
                        
                        node.University.Fields.Add(GetKey(), translator.Translate(node.University.Name, Translator.Languages.Uk, Translator.Languages.En));

                        foreach (XElement faculty in university.Elements())
                        {
                            node.Faculty.Name = faculty.Attribute("name").Value;
                            
                            node.Faculty.Fields.Add(GetKey(), translator.Translate(node.Faculty.Name, Translator.Languages.Uk, Translator.Languages.En));

                            foreach(XElement speciality in faculty.Elements())
                            {
                                node.Speciality.Name = speciality.Attribute("name").Value;
                                
                                node.Speciality.Fields.Add(GetKey(), translator.Translate(node.Speciality.Name, Translator.Languages.Uk, Translator.Languages.En));
                                
                                node.FacultyToSpeciality.SpecialityId = node.Speciality.Id;
                               
                                node.FacultyToSpeciality.FacultyId = node.Faculty.Id;

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
