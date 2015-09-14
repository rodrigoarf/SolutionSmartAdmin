using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing; 
using SmartAdmin.WebUI.Infrastructure.Session;    

namespace SmartAdmin.WebUI.Infrastructure.ActionFilters
{
    public class AuthorizedUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Session = new SessionManager();

            if (Session.IsActive() == false)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }

        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);

        //    if (filterContext == null)
        //    {
        //        throw new ArgumentNullException("filterContext");
        //    }

        //    List<string> allowedControllers = new List<string>() { "SecurityController" };
        //    List<string> allowedActions = new List<string>() { "Index" };

        //    string controllerName = filterContext.Controller.GetType().Name;
        //    string actionName = filterContext.ActionDescriptor.ActionName;

        //    if (!allowedControllers.Contains(controllerName)
        //    || !allowedActions.Contains(actionName))
        //    {
        //        filterContext.Result = View("UnauthorizedAccess");
        //    }
        //}  

    }
}




