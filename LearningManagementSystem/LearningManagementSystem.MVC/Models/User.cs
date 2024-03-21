using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.MVC.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        [RegularExpression(@"^([A-z0-9À-ž\s]){3,30}$", ErrorMessage = "User Name should contain Min 3 and Max 30 Characters")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+@_.-]{6,20}$", ErrorMessage = "Password should contain Min 6  & Max 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email Address, Ex : ben@gmail.com ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [RegularExpression(@"^([A-z0-9À-ž\s]){4,50}$", ErrorMessage = "Address should contain Min 4  & Max 50 characters")]
        public string UserAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number should contain 10 digits")]
        public string PhoneNumber { get; set; }
    }
}
