using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Users
{
    public class UserPostModel
    {
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{3,}$", ErrorMessage = "name starts with Capital letter and has minimum 3 characters")]
        public string firstName { get; set; }
       
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{3,}$", ErrorMessage = "name starts with Capital letter and has minimum 3 characters")]
        public string lastName { get; set; }

        [Required]
        [RegularExpression("^[a-z]{3,}[@][a-z]{4,}[.][a-z]{3,}$", ErrorMessage = "Please Enter a Valid Email")]
        public string email { get; set; }

        [Required]
        [RegularExpression("^[a-z]{3,}[0-9]{1,}[$]$", ErrorMessage = "Please Enter a Valid Password")]
        public string password { get; set; }
       
    }
}
