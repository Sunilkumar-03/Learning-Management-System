using LearningManagementSystem.API.Entities;
using LearningManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private IAdminRepository<Course> courseRepo;
        private ICourseRepository course;
        public CourseController(IAdminRepository<Course> _courseRepo, ICourseRepository courseRepository)
        {
            this.courseRepo = _courseRepo;
            course = courseRepository;
        }
        [HttpGet]
        [Route("GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            try
            {
                IEnumerable<Course> courses = courseRepo.GetAll();
                if (courses == null)
                {
                    return NotFound();
                }
                return Ok(courses);
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpPost]
        [Route("AddCourse")]
        public IActionResult AddCourse(Course course)
        {
            try
            {
                courseRepo.Add(course);
                return Ok("Added Successfully");
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteCourse/{Id}")]
        public IActionResult DeleteCourseConfirmed(int Id)
        {
            try
            {
                course.DeleteCourse(Id);        //DeleteCourse Function in CourseRepository.
                return Ok("Deleted Successfully");
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpPut]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(Course course)
        {
            try
            {
                courseRepo.Update(course);
                return Ok("Updated Successfully");
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("CourseDetail/{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                Course course = courseRepo.GetDetails(id); //"GetDetails" in Admin Repository.
                if (course == null)
                {
                    return NotFound();
                }
                return Ok(course);
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }


        //CourseRepository Function to Search for a Course For Admin to search and delete that course.

        [HttpGet]
        [Route("GetCourseName/{name}")]
        public IActionResult SearchCourseName(string name)
        {
            try
            {
                Course getCourse = course.GetCourseName(name);
                if (getCourse != null)
                    return Ok(getCourse);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
