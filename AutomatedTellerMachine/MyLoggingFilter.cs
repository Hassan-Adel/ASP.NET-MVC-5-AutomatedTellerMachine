using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine
{
    /*
     you can create a custom action filter by following these simple steps. 
     1- Create a class that inherits from the action filter attribute class.
     2- Then, override the on action executing method for code that should run just before the action.
     Or the on action executed method for code that should run just after the action. 
     
     Example : 
     And here's what it would look like. Let's say I wanted to create a filter that would log all
     successful requests for an action that it's applied to. 
         */
    public class MyLoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            // Logger.logRequest(request.UserHostAddress);
            base.OnActionExecuted(filterContext);
        }

    }
}