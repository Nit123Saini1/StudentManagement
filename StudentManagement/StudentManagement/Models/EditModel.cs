using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class EditModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Class")]
        public int ClassId { get; set; }

        public string Marks { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Required]
        public int StdSubMarkID { get; set; }
    }
}
