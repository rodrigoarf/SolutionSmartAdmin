using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;

namespace SmartAdmin.WebUI.Controllers.Banco
{
    public class BancoController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index()
        {
            return View();
        }

    }
}
