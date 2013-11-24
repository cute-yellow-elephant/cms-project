using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ApointMvcApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Вы ничего не ввели в поле с логином/email.")]
        [Display(Name = "Логин или email:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Вы ничего не ввели в поле с паролем.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?:")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Вы ничего не ввели в поле с логином.")]
        [Display(Name = "Логин:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Вы ничего не ввели в поле с паролем.")]
        [StringLength(12, ErrorMessage = "В пароле должно быть минимум {2} цифр.", MinimumLength = 6), MaxLength(12)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль (минимум 6 цифр, максимум - 12):")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль:")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Вы ничего не ввели в поле с email.")]
        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Email - неверный формат.")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }

}
