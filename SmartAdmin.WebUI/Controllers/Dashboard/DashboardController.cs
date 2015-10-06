using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.Dto;
using SmartAdmin.Domain;
using SmartAdmin.WebUI.ModelView;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;

namespace SmartAdmin.WebUI.Controllers.Dashboard
{
    public class DashboardController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index()
        {
            return View();
        }
    }
}
