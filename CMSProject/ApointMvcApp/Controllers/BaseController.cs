using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppCore;
using ApointMvcApp.Providers;
using System.Web.Security;
using Infrastructure;

namespace ApointMvcApp.Controllers
{
    public class BaseController : Controller
    {
        protected CoreHolder core;
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public BaseController()
            : base()
        { 
            core = new CoreHolder();
            Membership.GetNumberOfUsersOnline();
        }

    }
}
