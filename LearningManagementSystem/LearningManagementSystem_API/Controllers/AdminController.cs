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
    //NEW CODE


    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminRepository<User> _userRepo;
        private ICourseRepository _courseRepo;
        public AdminController(IAdminRepository<User> userRepo, ICourseRepository courseRepo)
        {
            this._userRepo = userRepo;
            this._courseRepo = courseRepo;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = _userRepo.GetAll();
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(User user)
        {
            try
            {
                if (_userRepo.CheckUserExists(user.Email))
                {
                    _userRepo.Add(user);
                    return Ok("Added Successfully");
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

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUserConfirmed(int id)
        {
            try
            {
                _userRepo.Delete(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _userRepo.Update(user);
                return Ok("Updated Successfully");
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("UserDetail/{id}")]
        public IActionResult UserDetails(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                User user = _userRepo.GetDetails(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }
        [HttpGet]
        [Route("SeeAllUserComplaints")]
        public IActionResult SeeAllUserComplaints()
        {
            try
            {
                List<UserCourseComplaint> adminVisibleComplaints = _courseRepo.SeeAllUserComplaints();
                if (adminVisibleComplaints.Count > 0)
                    return Ok(adminVisibleComplaints);
                else
                    return NotFound();
            }
            catch (Exception exception)
            {

                return Content(exception.Message);
            }
        }

        [HttpPut]
        [Route("ResolveComplaint")]
        public IActionResult ResolveComplaint(UserCourseComplaint obj)
        {
            try
            {
                bool resolvecheck = _courseRepo.ResolveComplaint(Convert.ToInt32(obj.UserID), Convert.ToInt32(obj.CourseID));
                if (resolvecheck == true)
                {
                    return Ok("Resolved!");
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
    }
}