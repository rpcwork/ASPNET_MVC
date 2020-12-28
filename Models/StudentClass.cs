using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperTest.Models
{
    public class StudentClass
    {
        [DisplayName("Class ID")]
        [Required(ErrorMessage = "Class ID is Required")]
        public string ClassID { get; set; }


        [DisplayName("Student ID")]
        [Required(ErrorMessage = "Student ID is Required")]
        public int StudentID { get; set; }


        [DisplayName("Student Class ID")]
        [Required(ErrorMessage = "Student Class ID is Required")]
        public int StudentClassID { get; set; }
    }
}