using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperTest.Models
{
    public class CourseClass
    {
        [DisplayName("Course ID")]
        [Required(ErrorMessage = "Course ID is Required")]
        public string CourseID { get; set; }

        [DisplayName("Instructor ID")]
        [Required(ErrorMessage = "Instructor ID is Required")]
        public int InstructorID { get; set; }

        [DisplayName("Class Day and Time")]
        [Required(ErrorMessage = "Class Day and Time is Required .e.g Tue 3p")]
        public String DateTime { get; set; }

        [DisplayName("Class ID")]
        [Required(ErrorMessage = " Class ID is Required")]
        public int ClassID { get; set; }
    }
}