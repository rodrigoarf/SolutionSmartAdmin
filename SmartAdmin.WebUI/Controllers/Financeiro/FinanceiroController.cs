using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using SmartAdmin.Domain;
using PagedList;
using SmartAdmin.Dto;

namespace SmartAdmin.WebUI.Controllers.Financeiro
{
    public class FinanceiroController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Boleto(int? Page)
        {
            //var BoletoDomain = new Boleto();
            //var Collection = BoletoDomain.GetList(_ => _.ID > 0);     
            //var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            //return View(Collection.ToPagedList(CurrentPage, PageSize));
            return View();
        }

        [AuthorizedUser]
        public ActionResult Cedente()
        {
            var CedenteDomain = new Cedente();
            var Model = CedenteDomain.GetItem(_ => _.ID == 1000);
            return View(Model);
        }

        [AuthorizedUser]
        public ActionResult Banco(int? Page)
        {
            var BancoDomain = new Banco();
            var Collection = BancoDomain.GetList(_=>_.ID > 0);  
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));

            ViewBag.Mensagem = (TempData["Mensagem"] as String);
            return View(Collection.ToPagedList(CurrentPage, PageSize));
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult SaveBanco(BancoDto Model)
        {           
            try
            {
                var BancoDomain = new Banco();
                BancoDomain.Save(Model);

                TempData["Mensagem"] = "Agência Bancaria <span style='color:#10e4ea;'>cadastrada</span> com sucesso";
                return (RedirectToAction("Banco", "Financeiro"));
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = "Erro ao cadastrar agência bancaria<br/>" + Ex.Message;
                return (RedirectToAction("Banco", "Financeiro"));
            }
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult DeleteBanco(int Id)
        {
            try
            {
                var BancoDomain = new Banco();
                BancoDomain.Delete(_ => _.ID == Id);

                TempData["Mensagem"] = "Agência Bancaria <span style='color:#10e4ea;'>apagada</span> com sucesso";
                return (RedirectToAction("Banco", "Financeiro"));
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = "Erro ao apagar agência bancaria<br/>" + Ex.Message;
                return (RedirectToAction("Banco", "Financeiro"));
            }
        }

    }
}
