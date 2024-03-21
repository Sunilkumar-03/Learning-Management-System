using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.MVC.Models
{
    public class UserCourseComplaint
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }

        [RegularExpression(@"^([A-z0-9À-ž\s]){4,200}$", ErrorMessage = "Description should contain Min 4 and Max 200 Characters")]
        public string ComplaintMessage { get; set; }
        public string ComplaintStatus { get; set; }
    }
}
