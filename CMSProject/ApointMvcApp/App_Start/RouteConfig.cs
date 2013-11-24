using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AppCore;

namespace ApointMvcApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("AdminMainPage","_admin/",new { controller = "Admin", action = "Index" });
            routes.MapRoute( "UserMainPage", "_user/", new { controller = "User", action = "Index" });
            routes.MapRoute("Login","Login/{returnUrl}", new { controller = "Account", action = "Login", returnUrl = UrlParameter.Optional });
            routes.MapRoute("Register","Register/{email}/{verifyingID}",new { controller = "Account",
                    action = "Register", email = UrlParameter.Optional, verifyingID = UrlParameter.Optional });
            routes.MapRoute("CreateRole","_admin/CreateRole",new { controller = "Admin", action = "CreateRole"});
            routes.MapRoute("DeleteRole", "_admin/DeleteRole", new { controller = "Admin", action = "DeleteRole" });
            routes.MapRoute("ViewAllRoles", "_admin/ViewAllRoles", new { controller = "Admin", action = "ViewAllRoles" });
            routes.MapRoute("CreateUser", "_admin/CreateUser", new { controller = "Admin", action = "CreateUser" });
            routes.MapRoute("DeleteUser", "_admin/DeleteUser", new { controller = "Admin", action = "DeleteUser" });
            routes.MapRoute("ViewAllUsers", "_admin/ViewAllUsers", new { controller = "Admin", action = "ViewAllUsers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{email}/{verifyingID}",
                defaults: new { controller = "Account", action = "Register", email = UrlParameter.Optional, verifyingID = UrlParameter.Optional });
        }
    }
}