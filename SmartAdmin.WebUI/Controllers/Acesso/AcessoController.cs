using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdmin.WebUI.Controllers.Acesso
{
    public class AcessoController : BaseController
    {    
        public ActionResult Index()
        {
            var AcessoDomain = unitOfWork.AcessoDomain;
            var Collection = AcessoDomain.GetList(_ => _.COD_USUARIO > 0).OrderByDescending(AcessoDto => AcessoDto.DTH_ACESSO);
            return View(Collection);
        }     
    }
}
