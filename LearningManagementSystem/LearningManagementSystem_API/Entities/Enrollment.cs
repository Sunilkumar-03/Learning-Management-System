using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LearningManagementSystem.API.Entities
{
    [Table("Enrollments")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Display(Name ="UserID")]
        public int UserID { get; set; }

      
        [Display(Name = "CourseID")]
        public int CourseID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(10)]
        public string CourseStatus { get; set; }

        [Required]
        [StringLength(10)]
        public string TestStatus { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }

    }
}
