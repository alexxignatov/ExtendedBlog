using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExtendedBlog.WebUI.Models
{
    public class RegisterModel
    {
        [Display(Name = "Ваш ник: ")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введите верный электронный адрес")]
        [Display(Name = "Адрес электронной почты:")]
        public string EMail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль не может быть меньше 6 и больше 100 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль: ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль: ")]
        [Compare("Password", ErrorMessage = "пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Телефон: ")]
        public string PhoneNumber { get; set; }
    }
}