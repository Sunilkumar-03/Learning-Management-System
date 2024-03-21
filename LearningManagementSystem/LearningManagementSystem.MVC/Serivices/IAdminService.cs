using LearningManagementSystem.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Serivices
{
    public interface IAdminService
    {
        bool AdminAddUser(User user);
        void AdminDeleteUser(int id);
        IEnumerable<User> GetAllUsersDetails();
        User UserDetailsById(int id);
        List<UserCourseComplaint> AdminGetAllComplaints();
        bool AdminResolveComplaint(UserCourseComplaint obj);
        List<CourseEnrollCount> CourseWiseReport();
        List<CourseCompletionDetails> CompletionWiseReport();

    }
}
