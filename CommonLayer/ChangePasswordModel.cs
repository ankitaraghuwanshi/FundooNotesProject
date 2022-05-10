using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class ChangePasswordModel
    {
        [Required]
        public string password { get; set; }

        [Required]
        public string conformPassword { get; set; }
        
    }
}
