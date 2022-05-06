using StudentManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class StudentModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public int ClassId { get; set; }
        [Required]
        public string ClassName { get; set; }

        
        public List<SubjectMarkModel> SubjectsAndMark { get; set; }
    }
}
