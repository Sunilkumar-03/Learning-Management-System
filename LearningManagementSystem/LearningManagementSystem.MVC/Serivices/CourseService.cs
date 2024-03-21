using LearningManagementSystem.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace LearningManagementSystem.MVC.Serivices
{
    public class CourseService : ICourseService
    {
        //ADD Course By Admin
        public void AdminAddCourse(Course course)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(course),
                System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("api/Course/AddCourse", contentData).Result;
            }
        }

        // DELETE Course By Admin
        public void AdminDeleteCourse(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                HttpResponseMessage response = client.DeleteAsync("api/Course/DeleteCourse/" + Id).Result;
            }
        }

        // UPDATE Course By Admin
        public void AdminUpdateCourse(Course courses) //"course" Changed to "courses"
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                var contentData = new StringContent(JsonConvert.SerializeObject(courses),
                System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("api/Course/UpdateCourse", contentData).Result;
                //if (response.IsSuccessStatusCode == true)
                //    return true;
                //else
                //    return false;
            }
        }

        //Get All Courses
        public List<Course> GetAllCoursesDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Course/GetAllCourses").Result;
                List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(response.Content.ReadAsStringAsync().Result);
                return courses;
            }
        }

        //Course Details by ID
        public Course CourseDetailsById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Course/CourseDetail/" + id).Result;
                Course course = JsonConvert.DeserializeObject<Course>(response.Content.ReadAsStringAsync().Result);
                return course;
            }
        }

        //"Search Course" - For Admin To Search and Delete That Course.
        public Course SearchCourseName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14521/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType); //add content type to the request header
                HttpResponseMessage response = client.GetAsync("api/Course/GetCourseName/" + name).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    Course course = JsonConvert.DeserializeObject<Course>(response.Content.ReadAsStringAsync().Result);
                    return course;
                }
                else
                    return null;
            }
        }


    }
}
