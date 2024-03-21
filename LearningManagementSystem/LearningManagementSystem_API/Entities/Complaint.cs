using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagementSystem.API.Entities
{
    [Table("Complaints")]
    public class Complaint
    {
        [Key]
        public int ComplaintID { get; set; }

        [Display(Name = "UserID")]
        public int UserID { get; set; }

        [Display(Name = "CourseID")]
        public int CourseID { get; set; }

        [Required]
        [MaxLength(300)]
        public string ComplaintMessage { get; set; }
        [StringLength(500)]
        public string ComplaintStatus { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        

        
    }
}
