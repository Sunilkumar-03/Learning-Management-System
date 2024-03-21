using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        [Required]
        public string CourseName { get; set; }

        [Required]
        public float CourseDuration { get; set; }

        [Required]
        public string CourseContent { get; set; }

        [Required]
        public string CourseQuiz { get; set; }
        [Required]
        public string CourseImage { get; set; }
        [Required]
        public string CourseDiscription { get; set; }
        [Required]
        public string CourseTutor { get; set; }
    }
}
