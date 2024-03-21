using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.MVC.Models
{
    public class Complaints
    {
        public int ComplaintID { get; set; }
        public int UserID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public string ComplaintMessage { get; set; }
        public string ComplaintStatus { get; set; }
    }
}
