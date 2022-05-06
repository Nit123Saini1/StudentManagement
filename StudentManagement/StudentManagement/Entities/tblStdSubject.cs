using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Entities
{
    public class tblStdSubject
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public tblStudent tblStudent { get; set; }

        public int SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public tblSubject tblSubject { get; set; }

        public string Marks { get; set; }


    }
}
