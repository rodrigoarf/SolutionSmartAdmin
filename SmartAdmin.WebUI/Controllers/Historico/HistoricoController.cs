using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.Dto;
using SmartAdmin.Domain;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;

namespace SmartAdmin.WebUI.Controllers
{
    public class HistoricoController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index(int? Page)
        {  
            var DataInicial = DateTime.Now.AddMonths(2);
            var DataTermino = DateTime.Now;
            
            var AcessoDominio = new SmartAdmin.Domain.Acesso();
            var UsuarioDominio = new SmartAdmin.Domain.Usuario();

            var CollectionAcesso = AcessoDominio.GetList(_ => _.DTH_ACESSO >= DataInicial || _.DTH_ACESSO <= DataTermino).Take(5000).ToList();
            var CollectionUsuario = UsuarioDominio.GetList(_ => _.STATUS == "A").Take(5000).ToList();

            foreach (var itemAcesso in CollectionAcesso)
            {
                if (itemAcesso.COD_USUARIO != null)
                {
                    foreach (var Usuario in CollectionUsuario)
                    {
                        if (itemAcesso.COD_USUARIO == Usuario.ID)
                        {
                            //itemAcesso.USUARIO = Usuario;
                        }
                    }                 
                }
            }

            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            return View(CollectionAcesso.ToPagedList(CurrentPage, PageSize));
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult Load(DateTime DataInicial, DateTime DataFinal)
        {
            var AcessoDominio = new SmartAdmin.Domain.Acesso();
            var Collection = new List<AcessoDto>();

            Collection = AcessoDominio.GetList(_ => _.DTH_ACESSO >= DataInicial &&
                                                    _.DTH_ACESSO <= DataFinal)
                                                     .OrderByDescending(_ => _.DTH_ACESSO).ToList();

            ViewBag.DataInicial = DataInicial;
            ViewBag.DataFinal = DataFinal;

            return View("Index", Collection.ToPagedList(1, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Limpar()
        {
            return View();
        } 
    }
}
