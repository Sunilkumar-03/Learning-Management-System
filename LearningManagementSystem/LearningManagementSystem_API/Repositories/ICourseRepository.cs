using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.API.Entities;

namespace LearningManagementSystem.API.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course courses); //"course" Changed to "courses"
        void DeleteCourse(int Id);
        Course GetCourse(int courseId);

        // "GetCourseName" - For both DeleteCourse And UpdateCourse Functions.
        Course GetCourseName(string name);
        List<UserCourseComplaint> SeeAllUserComplaints();
        bool ResolveComplaint(int userid, int courseid);

    }
}
