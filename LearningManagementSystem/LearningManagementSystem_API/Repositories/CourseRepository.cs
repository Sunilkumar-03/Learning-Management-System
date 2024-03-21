using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.API.Context;
using LearningManagementSystem.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private DBContext _context = null;

        public CourseRepository(DBContext context)
        {
            _context = context;
        }

        //Add Course by ADMIN
        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        //Delete Course By ADMIN using CourseName.
        public void DeleteCourse(int Id)
        {

            Course course = _context.Courses.Find(Id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

        //Update Course Details By ADMIN.
        public void UpdateCourse(Course courses)  //"course" Changed to "courses"
        {
            _context.Courses.Update(courses);
            _context.SaveChanges();
        }

        //Displays All Course Details
        public List<Course> GetAllCourses()
        {
            List<Course> courses = _context.Courses.ToList();
            return courses;

        }

        public Course GetCourse(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        //Search for a Course by using Course Name.
        public Course GetCourseName(string name)
        {
            Course course = _context.Courses.Where(item => item.CourseName.Contains(name)).Select(item => item).FirstOrDefault();
            return course;


        }

        //For Admin To View All Complaints raised by Users.
        public List<UserCourseComplaint> SeeAllUserComplaints()
        {
            List<UserCourseComplaint> adminVisibleComplaints = new List<UserCourseComplaint>();
            List<Complaint> complaints = _context.Complaints.ToList();
            foreach (var complaint in complaints)
            {
                UserCourseComplaint adminVisibleComplaint = new UserCourseComplaint();
                adminVisibleComplaint.UserID = complaint.UserID;
                adminVisibleComplaint.UserName = _context.Users.Where(i => i.UserID == complaint.UserID).Select(i => i.UserName).ToList()[0];
                adminVisibleComplaint.CourseID = complaint.CourseID;
                adminVisibleComplaint.CourseName = _context.Courses.Where(i => i.CourseID == complaint.CourseID).Select(i => i.CourseName).ToList()[0];
                adminVisibleComplaint.ComplaintMessage = complaint.ComplaintMessage;
                adminVisibleComplaint.ComplaintStatus = complaint.ComplaintStatus;
                adminVisibleComplaints.Add(adminVisibleComplaint);
            }
            return adminVisibleComplaints;
        }

        //For Admin To Resolve All Pending Complaints and make User Enroll to that Course.
        public bool ResolveComplaint(int userId, int courseId)
        {
            if (_context.Enrollments.Any(i => i.UserID == userId & i.CourseID == courseId) == false)
            {
                Enrollment enrollment = new Enrollment();
                enrollment.UserID = userId;
                enrollment.CourseID = courseId;
                enrollment.StartDate = DateTime.Today;
                enrollment.EndDate = DateTime.Today.AddDays(10);
                enrollment.CourseStatus = "Pending";
                enrollment.TestStatus = "Pending";
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                Complaint complaint = (Complaint)_context.Complaints.SingleOrDefault(i => i.UserID == userId && i.CourseID == courseId);
                complaint.ComplaintStatus = "Resolved";
                _context.Complaints.Update(complaint);
                _context.SaveChanges();
                return true;
            }
            else
            {
                Complaint complaint = (Complaint)_context.Complaints.SingleOrDefault(i => i.UserID == userId && i.CourseID == courseId);
                complaint.ComplaintStatus = "Resolved";
                _context.Complaints.Remove(complaint);
                _context.SaveChanges();
                return false;
            }
        }


    }
}
