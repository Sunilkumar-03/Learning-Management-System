using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.API.Entities;
using LearningManagementSystem.API.Context;

namespace LearningManagementSystem.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DBContext _context = null;

        public UserRepository(DBContext context)
        {
            _context = context;
        }

        //For User To Enroll for a particular Course.
        public bool EnrollCourse(Enrollment enrollment)
        {
            if (_context.Enrollments.Any(item => item.CourseID == enrollment.CourseID && item.UserID == enrollment.UserID))
                return false;
            else
            {
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                return true;
            }
        }

        //For User to search a Course using Course Name.
        public List<Course> SearchCourse(string Name)
        {
            List<Course> courses = _context.Courses.Where(item => item.CourseName.Contains(Name)).Select(item => item).ToList();
            if (courses != null)
                return courses;
            else
                return null;
        }

        public User UserLogin(string UserName, string pwd)
        {
            User user= _context.Users.SingleOrDefault(i=>i.Email==UserName && i.Password==pwd);
            return user;
        }

        //For User to Raise a Complaint for a Course.
        public bool MakeComplaint(Complaint complaint)
        {
            if ((_context.Enrollments.Any(m => m.CourseID == complaint.CourseID && m.UserID == complaint.UserID)) || (_context.Complaints.Any(m => m.CourseID == complaint.CourseID && m.UserID == complaint.UserID)))
            {
                return false;
            }
            else
            {
                _context.Complaints.Add(complaint);
                _context.SaveChanges();
                return true;
            }
        }

        //View List of Complaints rasied by a User in User Dashboard.
        //public List<Complaint> GetAllComplaints(int userId)
        //{
        //    return _context.Complaints.Where(i => i.UserID == userId).Select(i => i).ToList();
        //}

        //List of All Courses.
        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        //Complaint Details with CourseName,Description,Status.
        public List<CourseComaplaint> complaintDetails(int userId)
        {
            List<Complaint> complaints = _context.Complaints.Where(i => i.UserID == userId).Select(i => i).ToList();
            List<Course> courses = _context.Courses.ToList();
            List<CourseComaplaint> details = (from c in courses
                           join co in complaints on c.CourseID equals co.CourseID
                           select new CourseComaplaint
                           {
                               CourseName = c.CourseName,
                               ComplaintMessage = co.ComplaintMessage,
                               ComplaintStatus = co.ComplaintStatus
                           }).ToList();
            return details;
        }

        //List of Enrolled Courses of a User with Course-Status "Pending"
        public List<Course> EnrolledCourses(int userId)
        {
            List<Course> enrolledCourses = new List<Course>();
            List<int> courseIdList = new List<int>();
            Course course = null;
            if (_context.Enrollments.Any(i => i.UserID == userId))
            {
                courseIdList = _context.Enrollments.Where(i => i.UserID == userId & i.CourseStatus.Equals("Pending")).Select(i => i.CourseID).ToList();
                if (courseIdList.Count() > 0)
                {
                    foreach (var courseId in courseIdList)
                    {
                        course = _context.Courses.Find(courseId);
                        enrolledCourses.Add(course);
                    }
                    return enrolledCourses;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }
        public List<Course> TakeQuizCourses(int userId)
        {
            List<Course> takeQuizCourses = new List<Course>();
            List<int> courseIdList = new List<int>();
            Course course = null;
            if (_context.Enrollments.Any(i => i.UserID == userId))
            {
                courseIdList = _context.Enrollments.Where(i => i.UserID == userId && i.CourseStatus.Equals("Completed") && i.TestStatus.Equals("Pending")).Select(i => i.CourseID).ToList();
                if (courseIdList.Count() > 0)
                {
                    foreach (var courseId in courseIdList)
                    {
                        course = _context.Courses.Find(courseId);
                        takeQuizCourses.Add(course);
                    }
                    return takeQuizCourses;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //Change Course Status to "Completed" in Enrollments Table.
        public bool ChangeCourseStatus(int UserId, int CourseId)
        {
            Enrollment enrollment = new Enrollment();
            enrollment =  _context.Enrollments.Single(item => item.UserID == UserId && item.CourseID == CourseId && item.CourseStatus == "Pending");
            if (enrollment != null)
            {
                enrollment.CourseStatus = "Completed";
                _context.Enrollments.Update(enrollment);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        //Change Test Status to "Completed" in Enrollments Table.
        public bool ChangeTestStatus(int UserId, int CourseId)
        {
            Enrollment enrollment = new Enrollment();
            enrollment = _context.Enrollments.Single(item => item.UserID == UserId && item.CourseID == CourseId && item.TestStatus == "Pending");
            if (enrollment != null)
            {
                enrollment.TestStatus = "Completed";
                _context.Enrollments.Update(enrollment);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public List<CourseEnrollCount> CourseWiseReport()
        {
            List<CourseEnrollCount> courseReports = new List<CourseEnrollCount>();
            List<Course> courses = _context.Courses.ToList();
            foreach (var course in courses)
            {
                CourseEnrollCount courseReport = new CourseEnrollCount();
                courseReport.CourseName = course.CourseName;
                courseReport.UserCount = _context.Enrollments.Where(i => i.CourseID == course.CourseID).Count();
                courseReports.Add(courseReport);
            }
            return courseReports;
        }
        public List<CourseCompletionDetails> CompletionWiseReport()
        {
            List<CourseCompletionDetails> completionReports = new List<CourseCompletionDetails>();
            List<Enrollment> enrollments = _context.Enrollments.ToList();
            foreach (var enrollment in enrollments)
            {
                CourseCompletionDetails completionReport = new CourseCompletionDetails();
                completionReport.UserName = _context.Users.Where(i => i.UserID == enrollment.UserID).Select(i => i.UserName).ToList()[0];
                completionReport.CourseName = _context.Courses.Where(i => i.CourseID == enrollment.CourseID).Select(i => i.CourseName).ToList()[0];
                completionReport.CourseStatus = enrollment.CourseStatus;
                completionReport.TestStatus = enrollment.TestStatus;
                completionReports.Add(completionReport);
            }
            return completionReports;
        }
        public List<CourseEnrollmentDetails> GetAllEnrolledCourses(int userId)
        {
            List<CourseEnrollmentDetails> allEnrolledCourses = new List<CourseEnrollmentDetails>();
            List<Enrollment> enrollments = new List<Enrollment>();
            Course course = null;
            if (_context.Enrollments.Any(i => i.UserID == userId))
            {
                enrollments = _context.Enrollments.Where(i => i.UserID == userId).ToList();
                if (enrollments.Count() > 0)
                {
                    foreach (var enrollment in enrollments)
                    {
                        CourseEnrollmentDetails courseMin = new CourseEnrollmentDetails();
                        course = _context.Courses.Find(enrollment.CourseID);
                        courseMin.CourseName = course.CourseName;
                        courseMin.CourseStatus = enrollment.CourseStatus;
                        courseMin.TestStatus = enrollment.TestStatus;
                        allEnrolledCourses.Add(courseMin);
                    }
                    return allEnrolledCourses;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }
    }
}
