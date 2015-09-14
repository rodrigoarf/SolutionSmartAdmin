using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;

namespace SmartAdmin.WebUI.Controllers
{
    public class NewsletterController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Grupos()
        {
            return View();
        }

        [AuthorizedUser]
        public ActionResult Campanhas()
        {
            return View();
        }

        [AuthorizedUser]
        public ActionResult Templates()
        {
            return View();
        }


    }
}
