﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;

namespace SmartAdmin.WebUI.Controllers.Acesso
{
    public class AcessoController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index()
        {
            var AcessoDomain = new SmartAdmin.Domain.Acesso();
            var Collection = AcessoDomain.GetList(_ => _.COD_USUARIO > 0).OrderByDescending(AcessoDto => AcessoDto.DTH_ACESSO);
            return View(Collection);
        }     
    }
}
