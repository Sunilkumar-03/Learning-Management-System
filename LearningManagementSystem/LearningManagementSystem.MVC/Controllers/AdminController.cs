using LearningManagementSystem.MVC.Serivices;
using LearningManagementSystem.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.MVC.Notification;

namespace LearningManagementSystem.MVC.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService _service = null;
        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult LogOff()
        {
            try
            {
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "LogOff" });
            }
        }

        [HttpGet]
        [Route("AdminDashBoard")]
        public IActionResult DashBoard()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "DashBoard" });
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = _service.GetAllUsersDetails();
                return View(users);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "GetAllUsers" });
            }
        }


        [HttpGet]
        [Route("Add")]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddUser(User user)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (_service.AdminAddUser(user))
                    {
                        Mail.SendMailToUser(user.Email, user.Password);
                        return RedirectToAction("GetAllUsers");
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
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "AddUser" });
            }
        }



        [HttpGet]
        public IActionResult ToDeleteUser(User user)
        {
            try
            {
                TempData["Error"] = TempData["Message"];
                return View(user);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "ToDelete" });
            }
        }


        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if(id != 0)
                {
                    User user = _service.UserDetailsById(Convert.ToInt32(id));
                    if (user == null)
                    {
                        ViewBag.ErrorMessage = $"   User with Id {id} is not available";
                        return View("ToDeleteUser");
                    }
                    else
                    {
                        TempData["Message"] = "Invalid";
                        return RedirectToAction("ToDeleteUser", user);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Search Field Can't be Empty";
                    return View("ToDeleteUser");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "DeleteUser" });
            }
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                _service.AdminDeleteUser(id);
                return RedirectToAction("GetAllUsers");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "ConfirmDelete" });
            }

        }

        [HttpGet]
        [Route("CourseReports")]
        public IActionResult CourseStatusReports()
        {
            try
            {
                List<CourseEnrollCount> course = _service.CourseWiseReport().ToList();
                return View(course);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "CourseStatusReports" });
            }
        }


        [HttpGet]
        [Route("CompletionReports")]
        public IActionResult CourseCompletionStatusReports()
        {
            try
            {
                List<CourseCompletionDetails> course = _service.CompletionWiseReport().ToList();
                return View(course);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "CourseCompletionStatusReports" });
            }
        }

        [HttpGet]
        [Route("GetComplaints")]
        public IActionResult GetAllUserComplaints()
        {
            try
            {
                try
                {
                    List<UserCourseComplaint> userCourseComplaints = _service.AdminGetAllComplaints().ToList();
                    if (userCourseComplaints != null)
                        return View(userCourseComplaints);
                    else
                        return View();
                }
                catch (Exception)
                {

                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "GetAllUserComplaints" });
            }
        }

        [HttpGet]
        [Route("ResolveComplaints")]
        public IActionResult ResolveUserComplaints(UserCourseComplaint obj)
        {
            try
            {
                _service.AdminResolveComplaint(obj);
                return RedirectToAction("GetAllUserComplaints");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { Message = ex.Message, Controller = "Admin", Action = "ResolveUserComplaints" });
            }

        }

    }
}
