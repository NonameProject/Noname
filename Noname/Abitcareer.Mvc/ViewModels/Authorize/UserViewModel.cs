using CultureEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abitcareer.Mvc.ViewModels.Authorize
{
    [MetadataType(typeof(UserViewModelAttributes))]
    public class UserViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class UserViewModelAttributes
    {

        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType = typeof(LocalizationResx))]
        [DataType(DataType.EmailAddress, ErrorMessage = null, ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(LocalizationResx))]
        [EmailAddress(ErrorMessage = null,ErrorMessageResourceName="EmailError", ErrorMessageResourceType=typeof(LocalizationResx))]
        public string Email { get; set; }

        
        [Required(ErrorMessageResourceName = "RequiredMessage", ErrorMessageResourceType=typeof(LocalizationResx))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
