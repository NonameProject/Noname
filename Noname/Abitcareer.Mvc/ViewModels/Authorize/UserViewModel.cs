using CultureEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Mvc.ViewModels.Authorize
{
    public class UserViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(LocalizationResx))]
        [DataType(DataType.EmailAddress, ErrorMessage = null, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(LocalizationResx))]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(LocalizationResx))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(LocalizationResx))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
