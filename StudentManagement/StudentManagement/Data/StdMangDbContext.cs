using Microsoft.EntityFrameworkCore;
using StudentManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Data
{
    public class StdMangDbContext:DbContext
    {

        public StdMangDbContext(DbContextOptions<StdMangDbContext> options):base(options)
        {

        }
        public DbSet<tblStudent> tblStudents { get; set; }
        
        public DbSet<tblClass> tblClasses { get; set; }
        public DbSet<tblSubject> tblSubjects { get; set; }
        public DbSet<tblStdSubject> tblStdSubjects { get; set; }
    }
}
