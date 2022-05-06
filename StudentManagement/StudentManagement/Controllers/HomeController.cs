using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StudentManagement.Data;
using StudentManagement.Entities;
using StudentManagement.IRepository;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStudentRepository studentRepository;
        public HomeController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            var data = studentRepository.getAllStudentDetails();
            
            return View(data);
        }

        public IActionResult Create()
        {
            var classes = studentRepository.getAllClasses();
            
            ViewBag.Class = classes;

            CreateModel student = new CreateModel();
            
            var subMarkList = new List<SubjectMarkModel>();
            foreach (var item in studentRepository.getAllSubjects())
            {
                var subMark = new SubjectMarkModel() { SubjectId = item.Id, SubjectName = item.Name};
                
                subMarkList.Add(subMark);
            }

            student.SubjectAndMark = subMarkList;
            return View(student);
        }
        [HttpPost]
        public IActionResult create(CreateModel model)
        {
            ViewBag.selectedError = "Please select subject";
            var selected = model.SubjectAndMark.Where(x => x.Selected == true).ToList();
            if(selected.Count() != 0)
            {
                ViewBag.selectedError = "";
                if (ModelState.IsValid)
                {
                    studentRepository.AddStudent(model);
                    return RedirectToAction("index");
                }
            }

           

            var classes = studentRepository.getAllClasses();
            
            ViewBag.Class = classes;

            CreateModel student = new CreateModel();

            var subMarkList = new List<SubjectMarkModel>();
            foreach (var item in studentRepository.getAllSubjects())
            {
                var subMark = new SubjectMarkModel() { SubjectId = item.Id, SubjectName = item.Name };

                subMarkList.Add(subMark);
            }
            student.SubjectAndMark = subMarkList;
            return View(student);

        }


        public IActionResult Delete(string Id)
        {
            studentRepository.Delete(Convert.ToInt32(Id));
            return RedirectToAction("index");
        }
        public IActionResult Edit(string submarkID)
        {
            var student = studentRepository.Edit(Convert.ToInt32(submarkID));
            ViewBag.Classes = studentRepository.getAllClasses();

            ViewBag.Subjects = studentRepository.getUnAssignSubjects(Convert.ToInt32(submarkID));


            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                studentRepository.EditPost(model);
                return RedirectToAction("index");
            }
            var student = studentRepository.Edit(model.StdSubMarkID);
            ViewBag.Classes = studentRepository.getAllClasses();

            ViewBag.Subjects = studentRepository.getUnAssignSubjects(model.StdSubMarkID);


            return View(student);
        }
    }
}
