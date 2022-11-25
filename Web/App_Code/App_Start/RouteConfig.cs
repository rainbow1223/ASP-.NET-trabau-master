using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace ASP
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);

            //routes.Ignore("{resource}.axd/{*pathInfo}");

            //routes.MapPageRoute("Profile", "user/{*code}", "~/user/profile.aspx");
            routes.MapPageRoute("NewProject", "projects/edit-project.aspx", "~/projects/new-project.aspx");

           // routes.MapPageRoute("default", "", "~/Default.aspx");
        }
    }
}
