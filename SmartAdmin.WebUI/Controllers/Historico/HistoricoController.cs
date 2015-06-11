using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdmin.WebUI.Controllers
{
    public class HistoricoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(bool clear)
        {
            if (clear == true)
            { 
                //-> clear database
            }

            return View();
        }

        public ActionResult Limpar()
        {
            return View();
        } 
    }
}
