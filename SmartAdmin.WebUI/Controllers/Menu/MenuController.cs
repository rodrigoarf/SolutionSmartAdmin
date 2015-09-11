using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Dto;
using SmartAdmin.Domain;
using PagedList;

namespace SmartAdmin.WebUI.Controllers
{
    public class MenuController : BaseController
    {
        public ActionResult Index(int? Page)
        {
            var MenuDominio = unitOfWork.MenuDomain;
            var Collection = MenuDominio.GetList(_ => _.ID > 0);
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            return View(Collection.ToPagedList(CurrentPage, PageSize));
        }

        public ActionResult Edit(int Id)
        {
            var MenuDominio = unitOfWork.MenuDomain;
            return View(MenuDominio.GetItem(_ => _.ID == Id));
        } 

        [HttpPost]
        public ActionResult Save(MenuDto Model)
        {
            if (ModelState.IsValid)
            {
                var MenuDominio = unitOfWork.MenuDomain; 
                if (Model.ID > 0) { MenuDominio.Edit(Model); } else { MenuDominio.Save(Model); }  
            }
            return RedirectToAction("Index", new { Page = 1 });
        }

        [HttpPost]
        public ActionResult Load(MenuDto Model)
        {
            var MenuDominio = unitOfWork.UsuarioDomain;
            var Collection = MenuDominio.GetList(_ => _.ID > 0); // Default Search

            if (!String.IsNullOrEmpty(Model.NOME))
                Collection.Where(_ => _.NOME.Contains(Model.NOME)).ToList();   
            if (!String.IsNullOrEmpty(Model.STATUS))
                Collection.Where(_ => _.STATUS == Model.STATUS).ToList();

            return View("Index", Collection.ToPagedList(1, PageSize));
        } 

    }
}
