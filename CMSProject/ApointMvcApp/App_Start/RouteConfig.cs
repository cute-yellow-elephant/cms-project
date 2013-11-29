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
            routes.MapRoute("Login","login/{returnUrl}", new { controller = "Account", action = "Login", returnUrl = UrlParameter.Optional });
            routes.MapRoute("Register","register/{email}/{verifyingID}",new { controller = "Account",
                    action = "Register", email = UrlParameter.Optional, verifyingID = UrlParameter.Optional });
            routes.MapRoute("CreateRole","_admin/create-role",new { controller = "Admin", action = "CreateRole"});
            routes.MapRoute("DeleteRole", "_admin/delete-role", new { controller = "Admin", action = "DeleteRole" });
            routes.MapRoute("ViewAllRoles", "_admin/view-all-roles", new { controller = "Admin", action = "ViewAllRoles" });
            routes.MapRoute("CreateUser", "_admin/create-user", new { controller = "Admin", action = "CreateUser" });
            routes.MapRoute("DeleteUser", "_admin/delete-user", new { controller = "Admin", action = "DeleteUser" });
            routes.MapRoute("ViewAllUsers", "_admin/view-all-users", new { controller = "Admin", action = "ViewAllUsers" });
            routes.MapRoute("XmlSitemap", "sitemap", new { controller = "SiteMap", action = "GetXmlMap" });
            routes.MapRoute("CreatePost", "_user/create-post", new { controller = "Blog", action = "CreatePost" });
            routes.MapRoute("BlogWork", "_user/dealing-with-blog", new { controller = "Blog", action = "BlogWork" });
            routes.MapRoute("ViewPost", "_user/dealing-with-blog/post-{id}", new { controller = "Blog", action = "ViewPost", id=UrlParameter.Optional });
            routes.MapRoute("EditPost", "_user/dealing-with-blog/post-{id}/edit", new { controller = "Blog", action = "EditPost", id = UrlParameter.Optional });
            routes.MapRoute("DeletePost", "_user/dealing-with-blog/post-{id}/delete", new { controller = "Blog", action = "DeletePost", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{email}/{verifyingID}",
                defaults: new { controller = "Account", action = "Register", email = UrlParameter.Optional, verifyingID = UrlParameter.Optional });
        }
    }
}