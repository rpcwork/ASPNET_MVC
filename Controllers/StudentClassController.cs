using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeveloperTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeveloperTest.Controllers
{
    public class StudentClassController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStudentClasses(int id)
        {
            var filepath = HttpContext.Server.MapPath("~/JSONData/studentclasses.json");
            string json = System.IO.File.ReadAllText(filepath);
            var studentclasses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentClass>>(json);

            var filepath2 = HttpContext.Server.MapPath("~/JSONData/instructors.json");
            string json2 = System.IO.File.ReadAllText(filepath2);
            var instructors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Instructor>>(json2);

            var filepath3 = HttpContext.Server.MapPath("~/JSONData/courses.json");
            string json3 = System.IO.File.ReadAllText(filepath3);
            var courses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Course>>(json3);


            var filepath4 = HttpContext.Server.MapPath("~/JSONData/courseclasses.json");
            string json4 = System.IO.File.ReadAllText(filepath4);
            var coursesclasses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CourseClass>>(json4);


            var scdict = new List<Dictionary<string, object>>();
            foreach (var cc in studentclasses)
            {
                if (cc.StudentID == id) { 
                    var courseclass = coursesclasses.Find(x => x.ClassID.ToString() == cc.ClassID.ToString());
                    var instructor = instructors.Find(x => x.InstructorID == courseclass.InstructorID);
                    var course = courses.Find(x => x.CourseID == courseclass.CourseID);

                    var scdictitem = new Dictionary<string, object>();
                    scdictitem.Add("StudentClassID", cc.StudentClassID.ToString());
                    scdictitem.Add("ClassID", courseclass.ClassID.ToString());
                    scdictitem.Add("CourseID", course.CourseID);
                    scdictitem.Add("CourseName", course.Name);
                    scdictitem.Add("InstructorName", instructor.FirstName + " " + instructor.LastName);
                    scdictitem.Add("DayTime", courseclass.DateTime);
                    
                    scdict.Add(scdictitem);
                }
            }

            string jsonout = Newtonsoft.Json.JsonConvert.SerializeObject(scdict, Formatting.Indented);

            return Content(jsonout.ToString(), "application/json");

        }

        [HttpGet]
        public ActionResult ListStudentClasses(int id)
        {
            // based on StudentID in url, add  student object to viewbag
            var filepath = HttpContext.Server.MapPath("~/JSONData/students.json");
            string json = System.IO.File.ReadAllText(filepath);
            var students = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Student>>(json);

            if (students.Exists(x => x.StudentID == id))
            {
                ViewBag.Student = students.Find(x => x.StudentID == id);
            }
            else
            {
                return View("~/Views/Shared/ErrorStudentNotFound.cshtml");
            }

            var filepath2 = HttpContext.Server.MapPath("~/JSONData/instructors.json");
            string json2 = System.IO.File.ReadAllText(filepath2);
            var instructors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Instructor>>(json2);

            var filepath3 = HttpContext.Server.MapPath("~/JSONData/courses.json");
            string json3 = System.IO.File.ReadAllText(filepath3);
            var courses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Course>>(json3);

            
            var filepath4 = HttpContext.Server.MapPath("~/JSONData/courseclasses.json");
            string json4 = System.IO.File.ReadAllText(filepath4);
            var coursesclasses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CourseClass>>(json4);


            List<SelectListItem> classitems = new List<SelectListItem>();
            foreach (var cc in coursesclasses)
            {
                var instructor = instructors.Find(x => x.InstructorID == cc.InstructorID);
                var course = courses.Find(x => x.CourseID == cc.CourseID);
                classitems.Add(new SelectListItem {
                    Value = cc.ClassID.ToString(), Text = cc.CourseID + " - " + course.Name + " : " + instructor.FirstName + " " + instructor.LastName + " : " + cc.DateTime
                });
            }
            var classOptions = new SelectList(classitems, "Value", "Text");
            ViewBag.classOptions = classOptions;


            // To Do:prepare data to display classes that student has already added

            // To Do:if student has already added class, remove from dropdown


            return View("~/Views/StudentClass/ListStudentClasses.cshtml");
        }

        [HttpPost]
        public ActionResult AddClassToStudent(Student student)
        {
            
            var filepath = HttpContext.Server.MapPath("~/JSONData/studentclasses.json");
            string json = System.IO.File.ReadAllText(filepath);
            var studentclasses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StudentClass>>(json);

            NameValueCollection nvc = Request.Form;
            StudentClass sc = new StudentClass();
            sc.ClassID = nvc["ClassID"];
            sc.StudentID = Int32.Parse(nvc["StudentID"]);
            sc.StudentClassID = studentclasses.Count();

            // To Do: make sure that student hasn't already added this class

            studentclasses.Add(sc);
            TempData["msg"] = string.Format("Class added successfully");
            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentclasses);
            System.IO.File.WriteAllText(filepath, json);

            var response = new { success = true, responseText = "Class added successfully" };
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(response), "application/json");

        }
    }
}