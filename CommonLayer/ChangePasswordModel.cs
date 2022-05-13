using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class ChangePasswordModel
    {
        [Required]
        [RegularExpression("^[a-z]{3,}[0-9]{1,}[$]$", ErrorMessage = "Please Enter a Valid Password")]
        public string password { get; set; }

        [Required]
        [RegularExpression("^[a-z]{3,}[0-9]{1,}[$]$", ErrorMessage = "Please Enter a Valid Password")]
        public string conformPassword { get; set; }
        
    }
}
