using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LearningManagementSystem.API.Entities
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(30)]
        public string CourseName { get; set; }

        [Required]
        public float CourseDuration { get; set; }

        [Required]
        [StringLength(500)]
        public string CourseContent { get; set; }

        [Required]
        [StringLength(300)]
        public string CourseQuiz { get; set; }

        [StringLength(200)]
        public string CourseImage { get; set; }
        [StringLength(500)]
        public string CourseDiscription { get; set; }
        [StringLength(30)]
        public string CourseTutor { get; set; }

        public IList<Enrollment> Enrollments { get; set; }
        public IList<Complaint> Complaints { get; set; }


    }
}

