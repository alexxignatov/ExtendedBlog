using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExtendedBlog.WebUI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Display(Name = "Выберите пол:")]
        public bool? Male { get; set; }

        [Display(Name = "Дата рождения:")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Адрес электронной почты:")]
        public string Email { get; set; }

        [Display(Name = "Телефон:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "О себе:")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
    }
}