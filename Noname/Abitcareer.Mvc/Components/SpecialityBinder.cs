using Abitcareer.Mvc.ViewModels.LocalizedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Abitcareer.Mvc.Components
{
    class SpecialityBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = new SpecialityViewModel();
            model.Id = bindingContext.ValueProvider.GetValue("Id").AttemptedValue;
            model.Name = bindingContext.ValueProvider.GetValue("Name").AttemptedValue;

            var dict = new Dictionary<int, int>();
            foreach (var item in model.Salaries)
            {
                int tmp = 0;
                int.TryParse(bindingContext.ValueProvider.GetValue("Salaries[" + item.Key + "]").AttemptedValue, out tmp);
                dict[item.Key] = tmp;
            }
            model.Salaries = dict;
            return model;
        }
    }
}
