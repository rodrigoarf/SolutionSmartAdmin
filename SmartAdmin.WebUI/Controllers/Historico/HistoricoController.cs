using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.Dto;

namespace SmartAdmin.WebUI.Controllers
{
    public class HistoricoController : BaseController
    {
        public ActionResult Index(int? Page)
        {  
            var DataInicial = DateTime.Now.AddMonths(2);
            var DataTermino = DateTime.Now;
            
            var AcessoDominio = unitOfWork.AcessoDomain;
            var UsuarioDominio = unitOfWork.UsuarioDomain;

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
