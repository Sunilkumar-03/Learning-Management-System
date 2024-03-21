using LearningManagementSystem.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Serivices
{
    public class AdminService : IAdminService
    {
        //Add User By Admin.
        public bool AdminAddUser(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/Admin/AddUser", contentData).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    return true;
                }
                else
                    return false;
            }
        }


        //Delete User by Admin.
        public void AdminDeleteUser(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                HttpResponseMessage response = client.DeleteAsync("api/Admin/DeleteUser/" + id).Result;
            }
        }


        //Get All Users.
        public IEnumerable<User> GetAllUsersDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Admin/GetAllUsers").Result;
                IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(response.Content.ReadAsStringAsync().Result);
                return users;
            }
        }


        //Get User Details By Id.
        public User UserDetailsById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Admin/UserDetail/" + id).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                    return user;
                }
                else
                    return null;
                
            }
        }


        //All Users Complaints.
        public List<UserCourseComplaint> AdminGetAllComplaints()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Admin/SeeAllUserComplaints").Result;
                if (response.IsSuccessStatusCode == true)
                {
                    List<UserCourseComplaint> getComplaint = JsonConvert.DeserializeObject<List<UserCourseComplaint>>(response.Content.ReadAsStringAsync().Result);
                    return getComplaint;
                }
                else
                    return null;
            }
        }

        //Resolve User Complaints By ADMIN.
        public bool AdminResolveComplaint(UserCourseComplaint obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(obj),
                System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("api/Admin/ResolveComplaint", contentData).Result;
                if (response.IsSuccessStatusCode == true)
                    return true;
                else
                    return false;
            }
        }

        //Course Completion Status Report in ADMIN.
        public List<CourseCompletionDetails> CompletionWiseReport()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("api/User/CompletionWiseReport").Result;
                List<CourseCompletionDetails> list = JsonConvert.DeserializeObject<List<CourseCompletionDetails>>(response.Content.ReadAsStringAsync().Result);
                return list;
            }
        }

        //CourseName with User Enroll Count Report in ADMIN.
        public List<CourseEnrollCount> CourseWiseReport()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("api/User/CourseWiseReport").Result;
                List<CourseEnrollCount> list = JsonConvert.DeserializeObject<List<CourseEnrollCount>>(response.Content.ReadAsStringAsync().Result);
                return list;
            }
        }


    }
}
