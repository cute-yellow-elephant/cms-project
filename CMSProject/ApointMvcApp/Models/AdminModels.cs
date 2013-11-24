using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ApointMvcApp.Models
{
    public class CreateRoleModel
    {
        [Required(ErrorMessage="Вы ничего не ввели.")]
        [Display(Name = "Введите имя роли:")]
        public string RoleName { get; set; }
    }

    public class DeleteUserModel
    {
        [Required(ErrorMessage="Вы ничего не ввели.")]
        [Display(Name = "Введите логин или email пользователя:")]
        public string Username { get; set; }
    }

    public class CreateUserModel
    {
        [Required(ErrorMessage="Вы ничего не ввели, попробуйте еще раз.")]
        [StringLength(30, ErrorMessage="Слишком длинные введенные данные.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Введенное вами значение не является email адресом")]
        [Display(Name = "Введите email человека, которому Вы хотите выслать приглашение стать пользователем:")]
        public string Email { get; set; }
    }
}