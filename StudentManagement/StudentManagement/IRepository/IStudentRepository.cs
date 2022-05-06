using StudentManagement.Entities;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.IRepository
{
    public interface IStudentRepository
    {
        List<StudentModel> getAllStudentDetails();

        List<tblClass> getAllClasses();

        List<tblSubject> getAllSubjects();
        void AddStudent(CreateModel model);
        void Delete(int stdMarkId);

        EditModel Edit(int stdMarkId);

        void EditPost(EditModel model);

        List<tblSubject> getUnAssignSubjects(int id);


    }
}
