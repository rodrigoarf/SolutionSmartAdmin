using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.WebUI.Infrastructure.Cache;  

namespace SmartAdmin.WebUI.Infrastructure.ActionFilters
{
    public class AuthorizeUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var HttpContext = filterContext.HttpContext;

            //if (HttpContext.Session[Utilities.SESSION_USUARIO_LOGADO] == null)
            //{
            //    var ContollerInharits = filterContext.Controller as Controller;
            //    ContollerInharits.HttpContext.Response.Redirect("~/Login/Index");
            //}
        }
    }
}




