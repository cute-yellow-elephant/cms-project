using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using System.Web.Mvc;

namespace ApointMvcApp.Additional
{

    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns a full qualified action URL
        /// </summary>
        public static string QualifiedAction(this UrlHelper url, string actionName, string controllerName, object routeValues = null)
        {
            return url.Action(actionName, controllerName, routeValues, url.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string QualifiedRoute(this UrlHelper url, string routeName, object routeValues = null)
        {
            return url.RouteUrl(routeName, routeValues, url.RequestContext.HttpContext.Request.Url.Scheme);
        }
    }
}