using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdmin.WebUI.Controllers.Boleto
{
    public class BoletoController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DadosDoCedente()
        {
            return View();
        }

    }
}
