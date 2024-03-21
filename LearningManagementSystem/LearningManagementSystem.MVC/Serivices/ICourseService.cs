using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.MVC.Models;

namespace LearningManagementSystem.MVC.Serivices
{
    public interface ICourseService
    {
        void AdminAddCourse(Course course);
        void AdminUpdateCourse(Course courses); //"course" Changed to "courses"
        void AdminDeleteCourse(int Id);
        Course SearchCourseName(string name); //For Admin To Search a Course And Delete OR Update that Course
        List<Course> GetAllCoursesDetails();
        Course CourseDetailsById(int id);

    }
}
