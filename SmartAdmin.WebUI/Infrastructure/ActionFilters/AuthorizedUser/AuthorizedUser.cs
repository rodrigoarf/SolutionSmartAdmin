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
    }  
}




