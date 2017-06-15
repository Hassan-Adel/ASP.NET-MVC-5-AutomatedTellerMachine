using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutomatedTellerMachine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");            
            
            //routes.MapRoute(name: "Serial number", url: "serial/{letterCase}", defaults: new { controller = "Home", action = "Serial", letterCase = "upper" });

            //let's say I don't want home to be part of the request path and I just want it to be invoked with / serial.
            //If that's the case, I can just use serial for the URL. But just as an example, 
            routes.MapRoute(
                name: "Serial number",
                //But just as an example, let's go ahead and add a parameter that will allow the user to request a different format, 
                //like a lower case serial number.
                //So I'm going to add a letter case place holder after serial/. 
                url: "serial/{letterCase}",
                // And we'll say that the default value for letter case will be, upper
                defaults: new { controller = "Home", action = "Serial", letterCase = "upper" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            


        }
    }
}
