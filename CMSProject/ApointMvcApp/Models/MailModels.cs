using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ApointMvcApp.Models
{
    public class ConfirmationModel
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public Guid ID { get; set; }
    }

}
