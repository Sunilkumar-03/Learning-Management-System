using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.MVC.Serivices;
using LearningManagementSystem.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using LearningManagementSystem.MVC.Notification;
using IronXL;


namespace LearningManagementSystem.MVC.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        public static int value = 2;
        public static int limit = 1;
        public static int score = 0;
        public static string path = "";
        public static int CourseId = 0;
        private IUserService _service = null;
        private ICourseService _courseService = null;
        private IAdminService _adminService = null;
        public UserController(IUserService service, ICourseService courseService, IAdminService adminService)
        {
            _service = service;
            _courseService = courseService;
            _adminService = adminService;
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Login" });
            }
        }

        [HttpGet]
        [Route("LogOff")]
        public IActionResult LogOff()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("HomePage", "Home");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "LogOff" });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Id, string Password)
        {

            try
            {
                if (Id != null && Password != null)
                {

                    if (Id == "Admin@gmail.com" && Password == "Admin@12")
                    {
                        return RedirectToAction("DashBoard", "Admin");
                    }

                    else
                    {

                        User user = _service.UserLogin(Id, Password);
                        if (user != null)
                        {
                            HttpContext.Session.SetInt32("UserId", user.UserID);
                            HttpContext.Session.SetString("UserName", user.UserName);
                            return RedirectToAction("UserHomePage", "User");
                        }
                        else
                        {
                            ViewBag.Error = "Invalid Credentials";
                            return View();
                        }

                    }
                }
                else
                {

                    ViewBag.Error = "Please give input";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Login" });
            }
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Register" });
            }
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_service.RegisterUser(user))
                    {
                        if (Mail.SendMailToUser(user.Email, user.Password))
                        {
                            ViewBag.Notify = "Login Credentails has been sent to your Mail. Please press  \" I am already member \" ";
                            return View();
                        }
                        else
                        {
                            ViewBag.Notify = "Account Created Succssefully. Due to some technical reason we can't send mail at this moment. ";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "User with same Mail Id already exists";
                        return View();
                    }


                }
                else
                    return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Register" });
            }
        }
        [HttpGet]
        [Route("UserDashBoard")]
        public IActionResult UserHomePage()
        {
            try
            {
                ViewBag.Name = HttpContext.Session.GetString("UserName");
                int Id = (int)HttpContext.Session.GetInt32("UserId");
                List<Course> courses = _service.GetEnrolledCourses(Id);
                return View(courses);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "UserHomePage" });
            }
        }

        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile()
        {
            try
            {
                int Id = (int)HttpContext.Session.GetInt32("UserId");
                User user = _adminService.UserDetailsById(Id);
                return View(user);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Profile" });
            }
        }
        [HttpPost]
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile(User user)
        {
            try
            {
               if(ModelState.IsValid == true)
                {
                    if (_service.UpdateUser(user))
                        return RedirectToAction("UserHomePage");
                    else
                        return RedirectToAction("UserHomePage");
                }
                else
                {
                    return View("Profile");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "UpdateProfile" });
            }
        }

        [HttpGet]
        [Route("SearchForCourse")]
        public IActionResult Search()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Seacrh" });
            }
        }
        [HttpPost]
        [Route("SearchForCourse")]
        public IActionResult Search(string SearchValue)
        {
            try
            {
                if (SearchValue != null)
                {
                    List<Course> courses = _service.courses(SearchValue);
                    if (courses != null)
                        return View("Search", courses);
                    else
                    {
                        ViewBag.Empty = "Search Not Found";
                        return View("Search");
                    }
                }
                else
                {
                    ViewBag.Empty = "Search Field Can't be Empty";
                    return View("Search");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "Search" });
            }
        }

        [HttpGet]
        [Route("ViewAndEnrollCourse")]
        public IActionResult ViewEnroll(int Id)
        {
            try
            {
                TempData["ErrorMsg"] = TempData["Error"];
                Course course = _service.Getcourse(Id);
                return View(course);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "ViewEnroll" });
            }
        }
        [HttpGet]
        [Route("EnrollCourse")]
        public IActionResult EnrollCourse(int Id)
        {
            try
            {
                Enrollment enrollment = new Enrollment()
                {
                    UserID = (int)HttpContext.Session.GetInt32("UserId"),
                    CourseID = Id,
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.AddDays(10).Date,
                    CourseStatus = "Pending",
                    TestStatus = "Pending"
                };
                if (_service.EnrollCourse(enrollment))
                    return RedirectToAction("UserHomePage");
                else
                {
                    TempData["Error"] = "Already Enrolled";
                    return RedirectToAction("ViewEnroll", new { Id });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "EnrollCourse" });
            }
        }
        [HttpGet]
        [Route("RaiseComplaint")]
        public IActionResult RaiseComplaint()
        {
            try
            {
                List<SelectListItem> genreList = _service.GetAllCourses().Select(n => new SelectListItem { Value = n.CourseID.ToString(), Text = n.CourseName }).ToList(); ;

                var genreTip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Course ---"
                };
                genreList.Insert(0, genreTip);
                TempData["ErrorMessage"] = TempData["Error"];
                ViewBag.generList = new SelectList(genreList, "Value", "Text");
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "RaiseComplaint" });
            }
        }
        [HttpPost]
        [Route("RaiseComplaint")]
        public IActionResult RaiseComplaint(Complaints complaint)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    List<SelectListItem> genreList = _service.GetAllCourses().Select(n => new SelectListItem { Value = n.CourseID.ToString(), Text = n.CourseName }).ToList(); ;

                    var genreTip = new SelectListItem()
                    {
                        Value = null,
                        Text = "--- Select Course ---"
                    };
                    genreList.Insert(0, genreTip);
                    ViewBag.generList = new SelectList(genreList, "Value", "Text");
                    complaint.UserID = (int)HttpContext.Session.GetInt32("UserId");
                    complaint.ComplaintStatus = "Pending";
                    if (_service.MakeComplaint(complaint))
                    {
                        ViewBag.Error = "Complaint Raised Successfully";
                        return View();
                    }
                    else
                    {
                        ViewBag.Error = "Already Raised Complaint";
                        return View();
                    }
                }
                else
                {
                    TempData["Error"] = "Fields can't be Empty";
                    return RedirectToAction("RaiseComplaint");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "RaiseComplaint" });
            }
        }
        [HttpGet]
        [Route("GetComplaints")]
        public IActionResult ViewComplaints()
        {
            try
            {
                int userId = (int)HttpContext.Session.GetInt32("UserId");
                List<CourseComplaint> courseComplaint = _service.complaintDetails(userId);
                if (courseComplaint != null)
                    return View(courseComplaint);
                else
                {
                    ViewBag.Error = "No Complaints Raised";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "ViewComplaints" });
            }
        }
        [HttpGet]
        [Route("CourseStatusChange")]
        public IActionResult CourseStatusChange(int Id)
        {
            try
            {
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                if (_service.ChangeCourseStatus(UserId, Id))
                    return RedirectToAction("TestHomePage");
                else
                    return RedirectToAction("UserHomePage");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "CourseStatusChange" });
            }
        }
        [HttpGet]
        [Route("TestHomePage")]
        public IActionResult TestHomePage()
        {
            try
            {
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                List<Course> courses = _service.GetCoursesForTest(UserId);
                return View(courses);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "TestHomePage" });
            }
        }
        [HttpGet]
        [Route("RedirctToTest")]
        public IActionResult RedirctToTest(int Id)
        {
            try
            {
                Course course = _courseService.CourseDetailsById(Id);
                path = course.CourseQuiz;
                CourseId = course.CourseID;
                return RedirectToAction("QuizView");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "RedirctToTest" });
            }
        }
        [HttpGet]
        [Route("ViewAllCourses")]
        public IActionResult ViewAllCourses()
        {
            try
            {
                List<Course> courses = _courseService.GetAllCoursesDetails();
                return View(courses);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "ViewAllCourses" });
            }

        }
        [HttpGet]
        public IActionResult QuizView()
        {
            try
            {
                QuizClass quiz = new QuizClass();
                var workBook = new WorkBook(@path);
                var workSheet = workBook.GetWorkSheet("Sheet1");
                quiz.QuizName = workSheet["H1"].ToString(); //Quiz Name Cell "H1"
                string questionNo = "A" + value.ToString();
                quiz.QNo = workSheet[questionNo].ToString();
                string questioncell = "B" + value.ToString();
                quiz.Question = workSheet[questioncell].ToString();
                string Option1 = "C" + value.ToString();
                quiz.Option_1 = workSheet[Option1].ToString();
                string Option2 = "D" + value.ToString();
                quiz.Option_2 = workSheet[Option2].ToString();
                string Option3 = "E" + value.ToString();
                quiz.Option_3 = workSheet[Option3].ToString();
                string Option4 = "F" + value.ToString();
                quiz.Option_4 = workSheet[Option4].ToString();
                string answercell = "G" + value.ToString();
                quiz.Answer = workSheet[answercell].ToString();
                return View(quiz);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "QuizView" });
            }
        }
        [HttpPost]
        [Route("QuizResult")]
        public IActionResult QuizResult(QuizClass quiz)
        {
            try
            {
                if (limit < 5)
                {
                    if (quiz.Answer == quiz.UserOption)
                    {
                        value += 1;
                        score += 1;
                        limit += 1;
                        return RedirectToAction("QuizView");
                    }
                    else
                    {
                        value += 1;
                        limit += 1;
                        return RedirectToAction("QuizView");
                    }

                }
                else
                {
                    if (score >= 3)
                    {
                        int UserId = (int)HttpContext.Session.GetInt32("UserId");
                        if (_service.ChangeTestStatus(UserId, CourseId))
                        {
                            Course course = _courseService.CourseDetailsById(CourseId);
                            ViewBag.Name = HttpContext.Session.GetString("UserName");
                            ViewBag.CourseName = course.CourseName;
                            ViewBag.Score = score;
                            TempData["Date"] = DateTime.Now.Date.ToString("MM/dd/yyyy");
                            score = 0;
                            limit = 1;
                            value = 2;
                            return View("Certificate");
                        }
                        else
                        {
                            ViewBag.Score = score;
                            score = 0;
                            limit = 1;
                            value = 2;
                            return View("Failed");
                        }
                    }
                    else
                    {
                        ViewBag.Score = score;
                        score = 0;
                        limit = 1;
                        value = 2;
                        return View("Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "QuizResult" });
            }
        }



        [HttpGet]
        [Route("GetEnrolledCoursesWithStatus")]
        public IActionResult GetEnrolledCoursesWithStatus()
        {
            try
            {
                int userId = (int)HttpContext.Session.GetInt32("UserId");
                List<CourseEnrollmentDetails> courses = _service.GetEnrolledCoursesWithStatus(userId);
                if (courses != null)
                    return View(courses);
                else
                {
                    ViewBag.Error = "No Courses Enrolled";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "User", Action = "GetEnrolledCoursesWithStatus" });
            }
        }
    }
}
