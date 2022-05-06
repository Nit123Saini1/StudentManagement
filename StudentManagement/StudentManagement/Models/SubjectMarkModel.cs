using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class SubjectMarkModel
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public bool Selected { get; set; }

        public string Marks { get; set; }
    }
}
