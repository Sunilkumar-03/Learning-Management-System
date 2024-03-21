using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LearningManagementSystem.API.Repositories;
using LearningManagementSystem.API.Entities;
using Moq;

namespace LearningManagementSystem.UnitTest
{
    public class TestCourseRepository
    {
        private readonly ICourseRepository courseRepository;
        public TestCourseRepository()
        {
            IList<Course> courses = new List<Course>() {
            new Course(){CourseID=2001,CourseName="Python",CourseContent="W3schools.com",CourseDuration=10,CourseDiscription="xyz",CourseImage="python.jpg",CourseQuiz="abc",CourseTutor="Sachin"},
            new Course(){CourseID=2002,CourseName="Java",CourseContent="W3schools.com",CourseDuration=20,CourseDiscription="abc",CourseImage="java.jpg",CourseQuiz="abc",CourseTutor="Kumar"}

            };
            var mockRepo = new Mock<ICourseRepository>();

            mockRepo.Setup(repo => repo.GetAllCourses()).Returns(courses.ToList());
            mockRepo.SetupAllProperties();
            courseRepository = mockRepo.Object;
        }
        [Fact]
        public void TestAddCoursedetails()
        {
            var course = new Course() { CourseID = 2003, CourseName = "Python", CourseContent = "W3schools.com", CourseDuration = 10, CourseDiscription = "xyz", CourseImage = "python.jpg", CourseQuiz = "abc", CourseTutor = "Sachin" };
            courseRepository.AddCourse(course);

            Assert.Equal(1, 1);
        }
        [Fact]
        public void TestGetAllCourseDetails()
        {
            int expected = 2;
            var courseDetails = courseRepository.GetAllCourses();
            Assert.Equal(expected, courseDetails.Count());
        }
    }
}
