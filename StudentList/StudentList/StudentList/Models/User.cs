using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentList.Models
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Логін")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введіть адресу електоронної пошти")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}