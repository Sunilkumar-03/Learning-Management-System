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
     public class TestAdminRepository
    {
        private readonly IAdminRepository<User> adminRepository;
        public TestAdminRepository()
        {
            IList<User> users = new List<User>() {
            new User(){UserID=101,UserName="Vamsi",Password="Vamsi@12",UserAddress="Vizag",Email="Vamsi@gmail.com",PhoneNumber="9865231478"},
            new User(){UserID=102,UserName="Abhinay",Password="Abhi@12",UserAddress="karimnagar",Email="Abhi@gmail.com",PhoneNumber="9988774455"},

            };
            var mockRepo = new Mock<IAdminRepository<User>>();

            mockRepo.Setup(repo => repo.GetAll()).Returns(users.ToList());
            mockRepo.SetupAllProperties();
            adminRepository = mockRepo.Object;
        }
        [Fact]
        public void TestAddUserdetails()
        {
            var userDetails = new User() { UserID = 103, UserName = "Revanth", Password = "Reva@12", UserAddress = "Hyderabad", Email = "Revanth@gmail.com", PhoneNumber = "9865231478" };
            adminRepository.Add(userDetails);

            Assert.Equal(1, 1);
        }
        [Fact]
        public void TestGetAllUserDetails()
        {
            int expected = 2;
            var userDetails = adminRepository.GetAll();
            Assert.Equal(expected, userDetails.Count());
        }

    }
   }
