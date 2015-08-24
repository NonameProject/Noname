using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Components
{
    class SpecialityAdvancedModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            int tmp = 0;
            var model = new SpecialityAdvancedViewModel();
            model.Id = bindingContext.ValueProvider.GetValue("Id").AttemptedValue;
            model.Name = bindingContext.ValueProvider.GetValue("Name").AttemptedValue;
            model.EnglishName = String.Format("\"{0}\"", bindingContext.ValueProvider.GetValue("EnglishName").AttemptedValue);
            int.TryParse(bindingContext.ValueProvider.GetValue("AdditionalCosts").AttemptedValue, out tmp);
            model.AdditionalCosts = tmp;
            int.TryParse(bindingContext.ValueProvider.GetValue("AdditionalIncome").AttemptedValue, out tmp);
            model.AdditionalIncome = tmp;
            var dictionary = new Dictionary<int, int>();
            foreach (var item in model.Salaries)
            {
                int.TryParse(bindingContext.ValueProvider.GetValue("Salaries.[" + item.Key + "]").AttemptedValue, out tmp);
                dictionary[item.Key] = tmp;
            }
            model.Salaries = dictionary;

            var newDictionary = new Dictionary<string, int>();
            foreach (var item in model.Prices)
            {
                int.TryParse(bindingContext.ValueProvider.GetValue("Prices.[" + item.Key + "]").AttemptedValue, out tmp);
                newDictionary[item.Key] = tmp;
            }
            model.Prices = newDictionary;

            int.TryParse(bindingContext.ValueProvider.GetValue("StartOfWorking").AttemptedValue, out tmp);

            model.StartOfWorking = tmp;

            return model;
        }
    }
}
