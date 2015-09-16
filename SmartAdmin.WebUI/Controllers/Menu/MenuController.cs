using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Dto;
using SmartAdmin.Domain;
using SmartAdmin.WebUI.Infrastructure.Session;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using PagedList;  

namespace SmartAdmin.WebUI.Controllers
{
    public class MenuController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index(int? Page)
        {
            var MenuDominio = new Menu(); 
            var Collection = MenuDominio.GetList(_ => _.COD_MENU_PAI == 0);
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            return View(Collection.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Edit(int Id)
        {
            var MenuDominio = new Menu(); 
            return View(MenuDominio.GetItem(_ => _.ID == Id));
        } 
                    
        [HttpPost]
        [AuthorizedUser]
        public ActionResult Save(MenuDto Model)
        {
            if (ModelState.IsValid)
            {
                var MenuDominio = new Menu(); 
                if (Model.ID > 0) { MenuDominio.Edit(Model); } else { MenuDominio.Save(Model); }  
            }
            return RedirectToAction("Index", new { Page = 1 });
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult Load(MenuDto Model)
        {
            var MenuDominio = new Menu();
            var Collection = new List<MenuDto>();

            if (!String.IsNullOrEmpty(Model.NOME))
                Collection = MenuDominio.GetList(null).Where(_ => _.NOME.Contains(Model.NOME)).ToList();

            if (!String.IsNullOrEmpty(Model.STATUS))
                Collection = MenuDominio.GetList(null).Where(_ => _.STATUS == Model.STATUS).ToList();

            if (String.IsNullOrEmpty(Model.NOME) && (String.IsNullOrEmpty(Model.STATUS)))
                Collection = MenuDominio.GetList(_ => _.ID > 0).OrderBy(_=>_.NOME).ToList();

            return View("Index", Collection.ToPagedList(1, PageSize));
        } 

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var Session = new SessionManager();
            var Model = Session.GetUsuario();
            return View("~/Views/Shared/_MenuPartial.cshtml", Model);
        }

    }
}
