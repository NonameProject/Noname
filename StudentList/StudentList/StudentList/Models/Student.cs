using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentList.Models
{
    [Table("Student")]
    public class Student
    {

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Довжина рядка повинна бути між 3 та 50 символами")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        [StringLength(50, ErrorMessage = "Довжина рядка повинна бути між 3 та 50 символами")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        [StringLength(50, ErrorMessage = "Довжина рядка повинна бути між 3 та 50 символами")]
        public string Faculty { get; set; }

        [Required]
        [Display(Name = "Група")]
        [StringLength(50, ErrorMessage = "Довжина рядка повинна бути між 3 та 50 символами")]
        public string Group { get; set; }

        public IEnumerable<Mark> Marks { get; set; }
    }
}