using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExtendedBlog.WebUI.Models
{
    public class LoginModel
    {
        [Display(Name = "Username *")]
        [Required(ErrorMessage="Login is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Password is required")]
        [Display(Name = "Password *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me: ")]
        public bool RememberMe { get; set; }
    }
}