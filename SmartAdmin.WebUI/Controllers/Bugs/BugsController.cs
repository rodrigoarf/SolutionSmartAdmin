using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;

namespace SmartAdmin.WebUI.Controllers.Bugs
{
    public class BugsController : Controller
    {
        [AuthorizedUser]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizedUser]
        public ActionResult Edit()
        {
            return View();
        }   
    }
}
