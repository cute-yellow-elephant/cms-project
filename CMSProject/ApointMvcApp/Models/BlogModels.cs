using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ApointMvcApp.Models
{
    public class PostModel
    {
        [Required(ErrorMessage="Необходимо заполнить поле с заголовком")]
        [Display(Name="Заголовок:")]
        public string Title { get; set; }
        [Display(Name = "Теги:")]
        [DataType(DataType.MultilineText)]
        public string Tags { get; set; }

        [Required(ErrorMessage = "Пустой пост, а смысл такой создавать? Переделать!")]
        [Display(Name = "Содержание:")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
    }
}