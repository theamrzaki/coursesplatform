using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoursesPlatform
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //function to test sessions
            //GlobalFilters.Filters.Add(new SessionsFilter());
        }
    }

    /// <summary>
    /// to test sessions on each request
    /// to see if session is alive or not
    /// </summary>
    public class SessionsFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (!(controllerName == "Home" && actionName == "login") &&
                !(controllerName == "Home" && actionName == "register") &&
                !(controllerName == "HR" && actionName == "error"))
            {
                if (filterContext.HttpContext.Session["Logged_in_user"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary(new { controller = "Home", action = "login"/*, error = "Session has expired , Please login again"*/ })
                   );
                    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
