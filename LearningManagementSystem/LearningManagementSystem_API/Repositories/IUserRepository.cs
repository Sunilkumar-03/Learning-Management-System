using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.API.Entities;


namespace LearningManagementSystem.API.Repositories
{
    public interface IUserRepository
    {
        User UserLogin(string UserName, string pwd);
        List<Course> SearchCourse(string Name);
        bool EnrollCourse(Enrollment enrollment);
        bool MakeComplaint(Complaint complaint);
        //List<Complaint> GetAllComplaints(int userId);
        List<Course> GetAllCourses();
        List<CourseComaplaint> complaintDetails(int userId);
        List<Course> EnrolledCourses(int userId);
        List<Course> TakeQuizCourses(int userId);
        bool ChangeCourseStatus(int UserId, int CourseId);
        bool ChangeTestStatus(int UserId, int CourseId);
        List<CourseEnrollCount> CourseWiseReport();
        List<CourseCompletionDetails> CompletionWiseReport();
        List<CourseEnrollmentDetails> GetAllEnrolledCourses(int userId);
    }
}
