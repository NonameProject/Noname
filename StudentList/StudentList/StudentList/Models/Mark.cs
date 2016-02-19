using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentList.Models
{
    [Table("Mark")]
    public class Mark
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Назва предмету")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Довжина рядка повинна бути між 3 та 50 символами")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Значення")]
        [Range(0, 100, ErrorMessage = "Недопустима оцінка")]
        public short Value { get; set; }

        public int StudentId { get; set; }

        public Student students { get; set; }
    }
}