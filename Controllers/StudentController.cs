using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeveloperTest.Models;
using Newtonsoft.Json.Linq;

namespace DeveloperTest.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListStudents()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            var filepath = HttpContext.Server.MapPath("~/JSONData/students.json");
            string json = System.IO.File.ReadAllText(filepath);
            var students = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Student>>(json);

            if(students.Exists(x => x.StudentID == student.StudentID))
            {
                //Student ID already exists return failure
                ModelState.AddModelError("StudentID", "This Student ID already exists");
                return View("~/Views/Student/ListStudents.cshtml", student);
            }
            else
            {
                students.Add(student);
                TempData["msg"] = string.Format("Student {0} added", student.StudentID);
                json = Newtonsoft.Json.JsonConvert.SerializeObject(students);
                System.IO.File.WriteAllText(filepath, json);
            }

            return View("~/Views/Student/ListStudents.cshtml");
        }
    }
}