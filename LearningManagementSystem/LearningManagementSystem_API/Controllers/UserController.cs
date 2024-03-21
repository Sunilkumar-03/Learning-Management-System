using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.API.Repositories;
using LearningManagementSystem.API.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LearningManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAdminRepository<User> userRepo = null;
        private IUserRepository _repository = null;

        public UserController(IUserRepository repository, IAdminRepository<User> _userRepo)
        {
            _repository = repository;
            userRepo = _userRepo;
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult ValidateUser(string UserId,string Password)
        {
            try
            {
                User user = _repository.UserLogin(UserId, Password);
                if (user != null)
                    return Ok(user);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return Content(ex.InnerException.Message);
            }
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(User user)
        {
            try
            {
                if (userRepo.CheckUserExists(user.Email))
                {
                    userRepo.Add(user);
                    return Ok("Record Added");
                }
                else
                {
                    return NotFound();
                }
              
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        [Route("SearchCourse/{Name}")]
        public IActionResult SeacrhCourse(string Name)
        {
            try
            {
                List<Course> courses = _repository.SearchCourse(Name);
                if (courses.Count >0)
                    return Ok(courses);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        [Route("EnrollCourse")]
        public IActionResult Enroll(Enrollment enrollment)
        {
            try
            {
                if (_repository.EnrollCourse(enrollment))
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        [Route("MakeComplaint")]
        public IActionResult MakeComplaint(Complaint complaint)
        {
            try
            {
                if (_repository.MakeComplaint(complaint))
                    return Ok("Complaint Added");
                else
                    return NotFound("Already registered");
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }

        //[HttpGet]
        //[Route("GetComplaints/{Id}")]
        //public IActionResult GetComplaints(int Id)
        //{
        //    try
        //    {
        //        List<Complaint> complaints = _repository.GetAllComplaints(Id);
        //        if (complaints.Count > 0)
        //        {
        //            return Ok(complaints);
        //        }
        //        else
        //            return NotFound();
        //    }
        //    catch (Exception exception)
        //    {

        //        return Content(exception.Message);
        //    }
        //}

        [HttpGet]
        [Route("GetCourses")]
        public IActionResult GetAllCourses()
        {
            try
            {
                List<Course> courses = _repository.GetAllCourses();
                if (courses.Count > 0)
                {
                    return Ok(courses);
                }
                else
                    return NotFound();
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("GetComplaintsById/{id}")]
        public IActionResult GetUserComplaints(int id)
        {
            try
            {
                List<CourseComaplaint> courseComaplaints = _repository.complaintDetails(id);
                if (courseComaplaints.Count > 0)
                {
                    return Ok(courseComaplaints);
                }
                else
                    return NotFound();
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("EnrolledCourses/{userId}")]
        public IActionResult EnrolledCourses(int? userId)
        {
            try
            {
                List<Course> enrolledCourses = _repository.EnrolledCourses(Convert.ToInt32(userId));
                if (enrolledCourses != null)
                {
                    return Ok(enrolledCourses);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("TakeQuizCourses/{userId}")]
        public IActionResult TakeQuizCourses(int? userId)
        {
            try
            {
                List<Course> takeQuizCourses = _repository.TakeQuizCourses(Convert.ToInt32(userId));
                if (takeQuizCourses != null)
                {
                    return Ok(takeQuizCourses);
                }
                else
                {
                    return NotFound("No Courses Enrolled Yet (or) Enrolled Courses are Not completed (or) Test already taken.");
                }
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("ChangeCourseStatus/{userId}/{courseId}")]
        public IActionResult ChangeCourseStatus(int userId,int courseId)
        {
            try
            {
                if (_repository.ChangeCourseStatus(userId, courseId))
                    return Ok("Status Updated");
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        [Route("ChangeTestStatus/{userId}/{courseId}")]
        public IActionResult ChangeTestStatus(int userId, int courseId)
        {
            try
            {
                if (_repository.ChangeTestStatus(userId, courseId))
                    return Ok("Status Updated");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        [Route("CourseWiseReport")]
        public IActionResult CourseWiseReport()
        {
            try
            {
                List<CourseEnrollCount> Reports = _repository.CourseWiseReport();
                return Ok(Reports);
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("CompletionWiseReport")]
        public IActionResult CompletionWiseReport()
        {
            try
            {
                List<CourseCompletionDetails> Reports = _repository.CompletionWiseReport();
                return Ok(Reports);
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("GetAllEnrolledCourses/{userId}")]
        public IActionResult GetAllEnrolledCourses(int? userId)
        {
            try
            {
                List<CourseEnrollmentDetails> allEnrolledCourses = _repository.GetAllEnrolledCourses(Convert.ToInt32(userId));
                if (allEnrolledCourses != null)
                {
                    return Ok(allEnrolledCourses);
                }
                else
                {
                    return NotFound("No Courses Enrolled Yet");
                }
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
    }
}
