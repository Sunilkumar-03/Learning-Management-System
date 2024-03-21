using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Please Select Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Select End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please Select Course Status")]
        public string CourseStatus { get; set; }

        [Required(ErrorMessage = "Please Select Test Status")]
        public string TestStatus { get; set; }
    }
}
