using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Data.Model;
using SmartAdmin.Domain;
using SmartAdmin.Domain.Model;
using SmartAdmin.WebUI.Infrastructure.Session;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using PagedList;

namespace SmartAdmin.WebUI.Controllers
{
    public class MenuController : BaseController
    {
        [Authorize]
        [AuthorizedUser]
        public ActionResult Index(int? Page)
        {
            var MenuDominio = new MenuSpecialized(); 
            var Collection = MenuDominio.GetList(_ => _.COD_MENU_PAI == 0);
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));

            ViewBag.Mensagem = TempData["Mensagem"] as String;
            return View(Collection.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Edit(int Id, int? Page)
        {
            var MenuDominio = new MenuSpecialized();
            var Model = MenuDominio.GetItem(_ => _.ID == Id);
            var CollectionSubMenu = MenuDominio.GetList(_ => _.COD_MENU_PAI == Id); 
            var ModelView = new SmartAdmin.WebUI.ModelView.MenuModelView();

            ModelView.Menu = Model;
            ModelView.CollectionSubMenu = CollectionSubMenu;

            return View(ModelView); 
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult Save(MenuDto Model)
        {
            if (ModelState.IsValid)
            {
                var MenuDominio = new MenuSpecialized();

                if (Model.ID > 0) 
                { 
                    MenuDominio.Edit(Model);
                    TempData["Mensagem"] = "Categoria de menu <span style='color:#10e4ea;'>atualizada</span> com sucesso!";
                } 
                else 
                { 
                    MenuDominio.Save(Model);
                    TempData["Mensagem"] = "Categoria de menu <span style='color:#10e4ea;'>adicionada</span> com sucesso!";
                }
            }

            return RedirectToAction("Index", new { Page = 1 });
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult Load(MenuDto Model)
        {
            var MenuDominio = new MenuSpecialized();
            var Collection = new List<MenuDto>();

            if (!String.IsNullOrEmpty(Model.NOME))
                Collection = MenuDominio.GetList(null).Where(_ => _.NOME.Contains(Model.NOME) && _.COD_MENU_PAI == 0).ToList();

            if (!String.IsNullOrEmpty(Model.STATUS))
                Collection = MenuDominio.GetList(null).Where(_ => _.STATUS == Model.STATUS && _.COD_MENU_PAI == 0).ToList();

            if (String.IsNullOrEmpty(Model.NOME) && (String.IsNullOrEmpty(Model.STATUS)))
                Collection = MenuDominio.GetList(_ => _.ID > 0 && _.COD_MENU_PAI == 0).OrderBy(_=>_.NOME).ToList();

            return View("Index", Collection.ToPagedList(1, PageSize));
        } 

        [ChildActionOnly]
        public ActionResult MainMenuSpecialized()
        {
            var Session = new SessionManager();
            var Model = Session.GetObjectFromSession();
            return View("~/Views/Shared/_MenuPartial.cshtml", Model);
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult SaveOrEditSubMenu(MenuDto Model)
        {
            var Retorno = String.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    var MenuDominio = new MenuSpecialized();
                    if (Model.ID > 0)
                    {
                        MenuDominio.Edit(Model);
                        Retorno = "Menu <span style='color:#10e4ea;'>atualizado</span> com Sucesso!";
                    }
                    else
                    {
                        MenuDominio.Save(Model);
                        Retorno = "Menu <span style='color:#10e4ea;'>salvo</span> com Sucesso!";
                    }
                }
                else { Retorno = "Erro ao inserir ou atualizar menu, contate o administrador!"; }
            }
            catch (Exception Ex)
            {
                Retorno = "Erro ao executar ação de Salva ou de Editar.<br/>Erro: " + Ex.Message;
                return Content(Retorno);
            }

            return Content(Retorno);
        }

        [AuthorizedUser]
        public PartialViewResult CreateSubMenuPartial(int IdItem, int IdSubItem)
        {
            var Model = new MenuDto() { ID = IdItem, COD_MENU_PAI = IdSubItem };
            return (PartialView(Model));
        }
                          
        [AuthorizedUser]
        public PartialViewResult EditSubMenuPartial(int IdItem, int IdSubItem)
        {
            var MenuDomain = new MenuSpecialized();
            var Model = MenuDomain.GetItem(_ => _.ID == IdItem && _.COD_MENU_PAI == IdSubItem);
            return (PartialView((Model == null) ? new MenuDto() : Model));
        }

        [AuthorizedUser]
        public PartialViewResult DeleteSubMenuPartial(int IdItem, int IdSubItem)
        {
            var MenuDomain = new MenuSpecialized();
            var Model = MenuDomain.GetItem(_ => _.ID == IdItem && _.COD_MENU_PAI == IdSubItem);
            return (PartialView((Model == null) ? new MenuDto() : Model));
        }

        [HttpPost]
        [AuthorizedUser]
        public string DeleteSubMenu(int IdItem, int IdSubItem)
        {
            var Retorno = String.Empty;
            try
            {
                var MenuDomain = new MenuSpecialized();
                MenuDomain.Delete(_ => _.ID == IdItem && _.COD_MENU_PAI == IdSubItem);

                Retorno = "Menu <span style='color:#10e4ea;'>apagado</span> com sucesso!";
            }
            catch (Exception Ex)
            {
                Retorno = "Erro ao apagar menu contate o administrador!<br/>Erro:" + Ex.Message;
                return (Retorno);
            } 
            return (Retorno);
        }
                   
        [AuthorizedUser]
        public ActionResult DeletaMenu(int IdItem)
        {
            try
            {
                var MenuUsuarioDomain = new MenuUsuarioSpecialized();
                MenuUsuarioDomain.Delete(_ => _.COD_MENU == IdItem); //<-- deleta o menu dos usuario que tem permissão para o mesmo
                  
                var MenuDomain = new MenuSpecialized();
                MenuDomain.Delete(_ => _.COD_MENU_PAI == IdItem && _.ID > 0); //<-- deleta primeiro os filho
                MenuDomain.Delete(_ => _.ID == IdItem && _.COD_MENU_PAI == 0); //<-- deleta por ultimo o pai

                TempData["Mensagem"] = "Categoria de menu <span style='color:#10e4ea;'>apaga</span> com sucesso!";
                return (RedirectToAction("Index", "Menu"));
            }
            catch
            {
                TempData["Mensagem"] = "Erro ao apagar menu ";
                return (RedirectToAction("Index","Menu"));
            }
        }    
    }
}
