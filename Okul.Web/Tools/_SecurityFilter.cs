using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FirmaApp.Web.Tools
{
    public class _SecurityFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if(HttpContext.Current.Session["Kullanici"] == null && controllerName != "Login")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                            new { controller = "Login", action = "Index" }));
            }
               
            base.OnActionExecuting(filterContext);
        }
    }
}