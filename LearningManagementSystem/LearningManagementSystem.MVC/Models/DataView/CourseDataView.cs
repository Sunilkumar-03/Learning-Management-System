using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Models.DataView
{
    public class CourseDataView
    {
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Please Enter Name ")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Please Enter Duration ")]
        [RegularExpression(@"^[0-9]{2}$")]
        public float CourseDuration { get; set; }

        [Required(ErrorMessage = "Please Paste Link ")]
        
        public string CourseContent { get; set; }

        [Required(ErrorMessage = "Please Enter Path ")]
        public string CourseQuiz { get; set; }
        [Required(ErrorMessage = "Please Uplaod Image ")]
        public IFormFile CourseImage { get; set; }
        [Required(ErrorMessage = "Please Enter Description ")]
        //[RegularExpression(@"^([A-z0-9À-ž\s]){10,400}$", ErrorMessage = "Description should contain Min 10 and Max 400 Characters")]
        public string CourseDiscription { get; set; }
        [Required(ErrorMessage = "Please Enter Trainer Name ")]
        public string CourseTutor { get; set; }
    }
}
