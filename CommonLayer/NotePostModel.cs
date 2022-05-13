using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class NotePostModel
    {   [Required]
        [RegularExpression(@"^[A-Za-z]{3,}$", ErrorMessage = "Please Enter a Valid Title")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]{3,}$", ErrorMessage = "Please Enter a Valid Description")]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]{3,}$", ErrorMessage = "Please Enter a Valid Colour")]
        public string Colour { get; set; }
        
       
        
    }
}
