using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.API.Entities
{
    public class CourseComaplaint
    {
        public string CourseName { get; set; }
        public string ComplaintMessage { get; set; }
        public string ComplaintStatus { get; set; }
    }
}
