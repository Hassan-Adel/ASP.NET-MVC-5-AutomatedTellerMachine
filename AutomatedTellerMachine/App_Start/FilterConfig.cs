using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            /*
              You can also apply a filter globally to every action in your application. 
              To do that, find the filter config class in the app start folder.
              Inside the register global filters method we can add this filter.
             */
            filters.Add(new MyLoggingFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
