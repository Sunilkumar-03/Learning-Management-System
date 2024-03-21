using LearningManagementSystem.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LearningManagementSystem.MVC.Serivices
{
    public class UserService : IUserService
    {
        //User Register in Signup Page.
        public bool RegisterUser(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(user), 
                    System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/User/Register", contentData).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode == true)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        //User Login in Login Page.
        public User UserLogin(string UserId, string pwd)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/Login?UserId="+UserId+"&Password="+pwd).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode == true)
                    return null;
                else
                {
                    User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                    return user;
                }
            }
        }

        //User Searches For a Course using Course Name.
        public List<Course> courses(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/SearchCourse/"+name).Result;
                if(response.IsSuccessStatusCode == true)
                {
                    List<Course> list = JsonConvert.DeserializeObject<List<Course>>(response.Content.ReadAsStringAsync().Result);
                    return list;
                }
                else
                    return null;
            }
        }

        //User gets Course Details by Id.
        public Course Getcourse(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Course/CourseDetail/" + Id).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    Course course = JsonConvert.DeserializeObject<Course>(response.Content.ReadAsStringAsync().Result);
                    return course;
                }
                else
                    return null;
            }
        }

        public bool EnrollCourse(Enrollment enrollment)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(enrollment),
                  System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/User/EnrollCourse", contentData).Result;
                if (response.IsSuccessStatusCode == true)
                    return true;
                else
                    return false;
            }
        }


        public bool MakeComplaint(Complaints complaints)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(complaints),
                    System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/User/MakeComplaint", contentData).Result;
                if (response.IsSuccessStatusCode == true)
                    return true;
                else
                    return false;
            }
        }

        public List<Course> GetAllCourses()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/GetCourses").Result;
                List<Course> list = JsonConvert.DeserializeObject<List<Course>>(response.Content.ReadAsStringAsync().Result);
                return list;
            }
        }

        public List<CourseComplaint> complaintDetails(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/GetComplaintsById/" + userId).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    List<CourseComplaint> list = JsonConvert.DeserializeObject<List<CourseComplaint>>(response.Content.ReadAsStringAsync().Result);
                    return list;
                }
                else
                    return null;
            }
        }

        public List<Course> GetEnrolledCourses(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/EnrolledCourses/" + userId).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    List<Course> list = JsonConvert.DeserializeObject<List<Course>>(response.Content.ReadAsStringAsync().Result);
                    return list;
                }
                else
                    return null;
            }
        }

        public bool ChangeCourseStatus(int UserId, int CourseId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/ChangeCourseStatus/" + UserId+"/"+CourseId).Result;
                if (response.IsSuccessStatusCode == true)
                    return true;
                else
                    return false;
            }
        }

        public bool ChangeTestStatus(int UserId, int CourseId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/ChangeTestStatus/" + UserId + "/" + CourseId).Result;
                if (response.IsSuccessStatusCode == true)
                    return true;
                else
                    return false;
            }
        }

        public List<Course> GetCoursesForTest(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/TakeQuizCourses/" + userId).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    List<Course> list = JsonConvert.DeserializeObject<List<Course>>(response.Content.ReadAsStringAsync().Result);
                    return list;
                }
                else
                    return null;
            }
        }

        public bool UpdateUser(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(user),
                System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("api/Admin/UpdateUser", contentData).Result;
                if (response.IsSuccessStatusCode == true)
                    return true;
                else
                    return false;
            }
        }
        public List<CourseEnrollmentDetails> GetEnrolledCoursesWithStatus(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/User/GetAllEnrolledCourses/" + userId).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    List<CourseEnrollmentDetails> list = JsonConvert.DeserializeObject<List<CourseEnrollmentDetails>>(response.Content.ReadAsStringAsync().Result);
                    return list;
                }
                else
                    return null;
            }
        }

    }
}