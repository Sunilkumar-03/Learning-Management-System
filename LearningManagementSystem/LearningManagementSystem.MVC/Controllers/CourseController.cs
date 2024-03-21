using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.MVC.Serivices;
using LearningManagementSystem.MVC.Models;
using LearningManagementSystem.MVC.Models.DataView;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LearningManagementSystem.MVC.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _service = null;
        private readonly IWebHostEnvironment _weHostEnvironment;

        public CourseController(ICourseService service, IWebHostEnvironment weHostEnvironment)
        {
            _service = service;
            _weHostEnvironment = weHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("AddCourse")]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [Route("AddCourse")]
        public IActionResult AddCourse(CourseDataView vm)
        {
            try
            {
                if(ModelState.IsValid==true)
                {
                    string fileName = UploadFile(vm);
                    Course course = new Course
                    {
                        CourseName = vm.CourseName,
                        CourseDuration = vm.CourseDuration,
                        CourseContent = vm.CourseContent,
                        CourseQuiz = vm.CourseQuiz,
                        CourseTutor = vm.CourseTutor,
                        CourseImage = fileName,
                        CourseDiscription = vm.CourseDiscription
                    };
                    _service.AdminAddCourse(course);
                    return RedirectToAction("GetAllCourses");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "AddCourse" });
            }
        }

        private string UploadFile(CourseDataView vm)
        {
            try
            {
                string fileName = null;
                if (vm.CourseImage != null)
                {
                    string UploadDir = Path.Combine(_weHostEnvironment.WebRootPath, "Layout\\img"); // Project Directory path
                    fileName = Guid.NewGuid().ToString() + vm.CourseImage.FileName;
                    string filePath = Path.Combine(UploadDir, fileName);   // Binding image to FileName
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        vm.CourseImage.CopyTo(fileStream);
                    }
                }
                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        [Route("GetCourses")]
        public IActionResult GetAllCourses()
        {
            try
            {
                List<Course> courses = _service.GetAllCoursesDetails();
                return View(courses);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "GetAllCourses" });
            }
        }


        [HttpGet]
        public IActionResult ToUpdateCourse(Course course)
        {
            try
            {
                TempData["ErrorMessage"] = TempData["Message"];
                Course courses = course;
                return View(courses);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "ToUpdateCourses" });
            }
        }

        [HttpPost]
        public IActionResult UpdateCourse(string name)
        {
            try
            {
                Course course = _service.SearchCourseName(name);
                if (course == null)
                {
                    ViewBag.ErrorMessage = $"Please Enter a valid Course Name.";
                    return View("ToUpdateCourse");
                }
                else
                {
                    TempData["Message"] = "Invalid Course Name";
                    return RedirectToAction("ToUpdateCourse", course);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "UpdateCourses" });
            }
        }


        [HttpPost]
        public IActionResult ConfirmUpdateCourse(Course courses) //"course" Changed to "courses"
        {
            try
            {
                _service.AdminUpdateCourse(courses);
                return RedirectToAction("GetAllCourses");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "ConfirmUpdateCourse" });
            }

        }

        [HttpGet]
        public IActionResult ToDeleteCourse(Course course)   //public IActionResult ToDelete(Course course)
        {
            try
            {
                TempData["ErrorMessage"] = TempData["Message"];
                return View(course);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "ToDeleteCourse" });
            }
        }


        [HttpPost]
        public IActionResult DeleteCourse(string name)
        {
            try
            {
                Course course = _service.SearchCourseName(name);
                if (course == null)
                {
                    ViewBag.ErrorMessage = $"Please Enter a valid Course Name.";
                    return View("ToDeleteCourse");
                }
                else
                {
                    TempData["Message"] = "Invalid Course Name";
                    return RedirectToAction("ToDeleteCourse", course);  //return RedirectToAction("ToDelete", course);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "DeleteCourse" });
            }
        }


        [HttpGet]
        public IActionResult ConfirmDeleteCourse(int Id)
        {
            try
            {
                _service.AdminDeleteCourse(Id);
                return RedirectToAction("GetAllCourses");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Course", Action = "ConfirmDeleteCourse" });
            }

        }

    }
}
