using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperTest.Models
{
    public class Course
    {

        [DisplayName("Course ID")]
        [Required(ErrorMessage = "Course ID is Required")]
        public string CourseID { get; set; }
        
        [DisplayName("Course Name")]
        [Required(ErrorMessage = "Course Name is Required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        
    }
}