using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo_project.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [Unique(ErrorMessage = "This Email already exist !!")]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
    }
}