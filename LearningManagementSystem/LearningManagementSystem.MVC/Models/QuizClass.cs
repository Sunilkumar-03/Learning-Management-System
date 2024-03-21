using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Models
{
    public class QuizClass
    {
        public string QuizName { get; set; }
        public string QNo { get; set; }
        public string Question { get; set; }
        public string Option_1 { get; set; }
        public string Option_2 { get; set; }
        public string Option_3 { get; set; }
        public string Option_4 { get; set; }
        public string Answer { get; set; }
        public string UserOption { get; set; }
    }
}
