using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using System.Text;
using ApointMvcApp.Models;

namespace ApointMvcApp.Controllers
{
    public class MailController : MailerBase
    {
        public EmailResult Confirmation(ConfirmationModel mailModel)
        {
            To.Add(mailModel.Email);
            Subject = "Регистрация на сайте APoint";
            MessageEncoding = Encoding.UTF8;
            return Email("Confirmation", mailModel);
        }

    }
}
