using StudentManagement.Data;
using StudentManagement.Entities;
using StudentManagement.IRepository;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StdMangDbContext stdMangDb;
        public StudentRepository(StdMangDbContext stdMangDb)
        {
            this.stdMangDb = stdMangDb;
        }

        public List<StudentModel> getAllStudentDetails()
        {
            
            var studentdetails = new List<StudentModel>();
            foreach (var item in stdMangDb.tblStudents.ToList())
            {
                var student = new StudentModel()
                {
                    Id=item.Id,
                    FirstName=item.FirstName,
                    LastName=item.LastName,
                    
                };

                var classes = stdMangDb.tblClasses.Where(x => x.Id == item.Class).FirstOrDefault();
                student.ClassId = classes.Id;
                student.ClassName = classes.Name;

                var subMarksList = new List<SubjectMarkModel>();
                foreach (var sub in stdMangDb.tblStdSubjects.Where(x => x.StudentId == item.Id).ToList())
                {
                    var subMarks = new SubjectMarkModel();
                    subMarks.Id = sub.Id;
                    subMarks.Marks = sub.Marks;

                    var subjects = stdMangDb.tblSubjects.Where(x => x.Id == sub.SubjectID).FirstOrDefault();
                    subMarks.SubjectId = subjects.Id;
                    subMarks.SubjectName = subjects.Name;

                    subMarksList.Add(subMarks);
                }
                student.SubjectsAndMark = subMarksList;
                
                studentdetails.Add(student);
            }

            return studentdetails;
        }
        public List<tblClass> getAllClasses()
        {
            return stdMangDb.tblClasses.ToList();
        }
        public List<tblSubject> getAllSubjects()
        {
            return stdMangDb.tblSubjects.ToList();
        }

        public List<tblSubject> getUnAssignSubjects(int id)
        {
            var data = stdMangDb.tblSubjects.ToList();
            var stdSubject = stdMangDb.tblStdSubjects.Where(x=>x.Id==id).FirstOrDefault();

            var subjectlist = new List<tblSubject>();
            foreach (var item in data)
            {
                
                var Temp = stdMangDb.tblStdSubjects.Where(x => x.SubjectID == item.Id && x.StudentId== stdSubject.StudentId ).FirstOrDefault();
                if(Temp == null || Temp.SubjectID == stdSubject.SubjectID)
                {
                    var subject = new tblSubject()
                    {
                        Id= item.Id,
                        Name=item.Name
                    };
                    subjectlist.Add(subject);
                }
                
            }
            return subjectlist;
        }

        public void AddStudent(CreateModel model)
        {
            stdMangDb.tblStudents.Add(new tblStudent { FirstName = model.FirstName, LastName = model.LastName, Class = model.Class });
            stdMangDb.SaveChanges();

            var stdId = stdMangDb.tblStudents.OrderByDescending(x=>x.Id).Select(x=>x.Id).FirstOrDefault();

            foreach(var item in model.SubjectAndMark)
            {
                if(item.Selected==true)
                {
                    stdMangDb.tblStdSubjects.Add(new tblStdSubject { StudentId = stdId, SubjectID = item.SubjectId,Marks=item.Marks });
                    stdMangDb.SaveChanges();
                }

            }
            
        }

        public void Delete(int stdMarkId)
        {
            var stdMarkData = stdMangDb.tblStdSubjects.Where(x => x.Id == stdMarkId).FirstOrDefault();

            var stdSubjectList = stdMangDb.tblStdSubjects.Where(x => x.StudentId == stdMarkData.StudentId).ToList();
            if(stdSubjectList.Count() > 1)
            {
                stdMangDb.tblStdSubjects.Remove(stdMarkData);
                stdMangDb.SaveChanges();
            }
            else
            {
                var student = stdMangDb.tblStudents.Where(x => x.Id == stdMarkData.StudentId).FirstOrDefault();

                stdMangDb.tblStdSubjects.Remove(stdMarkData);
                stdMangDb.SaveChanges();
                stdMangDb.tblStudents.Remove(student);
                stdMangDb.SaveChanges();
            }

            
        }
        public EditModel Edit(int stdMarkId)
        {
            var stdMarkData = stdMangDb.tblStdSubjects.Where(x => x.Id == stdMarkId).FirstOrDefault();
            var student = stdMangDb.tblStudents.Where(x => x.Id == stdMarkData.StudentId).FirstOrDefault();

            var std = new EditModel()
            {
                Id=student.Id,
                FirstName=student.FirstName,
                LastName=student.LastName,
                ClassId= student.Class,
                SubjectId= stdMarkData.SubjectID,
                Marks= stdMarkData.Marks,
                StdSubMarkID= stdMarkData.Id

            };
            return std;
        }

        public void EditPost(EditModel model)
        {
            var student = stdMangDb.tblStudents.Where(x => x.Id == model.Id).FirstOrDefault();
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Class = model.ClassId;

            stdMangDb.tblStudents.Update(student);
            stdMangDb.SaveChanges();

            var stdMarkData = stdMangDb.tblStdSubjects.Where(x => x.Id == model.StdSubMarkID).FirstOrDefault();
            stdMarkData.SubjectID = model.SubjectId;
            stdMarkData.Marks = model.Marks;

            stdMangDb.tblStdSubjects.Update(stdMarkData);
            stdMangDb.SaveChanges();

        }
    }
}
