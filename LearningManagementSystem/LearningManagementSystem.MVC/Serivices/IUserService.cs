using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.MVC.Models;

namespace LearningManagementSystem.MVC.Serivices
{
    public interface IUserService
    {
        User UserLogin(string UserId, string pwd);
        bool RegisterUser(User user);
        List<Course> courses(string name);  //Search For a Course List Type.
        Course Getcourse(int Id);
        bool EnrollCourse(Enrollment enrollment);
        bool MakeComplaint(Complaints complaint);
        List<Course> GetAllCourses();
        List<CourseComplaint> complaintDetails(int userId);
        List<Course> GetEnrolledCourses(int userId);
        List<Course> GetCoursesForTest(int userId);
        bool ChangeCourseStatus(int UserId, int CourseId);
        bool ChangeTestStatus(int UserId, int CourseId);
        bool UpdateUser(User user);
        List<CourseEnrollmentDetails> GetEnrolledCoursesWithStatus(int userId);
    }
}
