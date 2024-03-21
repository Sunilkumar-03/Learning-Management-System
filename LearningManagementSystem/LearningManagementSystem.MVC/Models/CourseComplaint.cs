using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.MVC.Models
{
    public class CourseComplaint
    {
        public string CourseName { get; set; }
        public string ComplaintMessage { get; set; }
        public string ComplaintStatus { get; set; }
    }
}
