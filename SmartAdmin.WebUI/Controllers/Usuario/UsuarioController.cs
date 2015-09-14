using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Dto;
using SmartAdmin.Domain;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using PagedList;

namespace SmartAdmin.WebUI.Controllers
{
    public class UsuarioController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index(int? Page)
        {
            var UsuarioDominio = unitOfWork.UsuarioDomain;
            var Model = UsuarioDominio.GetList(_ => _.ID > 0);

            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            return View(Model.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Edit(int Id)
        {
            var UsuarioDominio = unitOfWork.UsuarioDomain;
            var Model = UsuarioDominio.GetItem(_ => _.ID == Id);
            return View(Model);
        }
              
        [HttpPost]
        [AuthorizedUser]
        public ActionResult Load(UsuarioDto Model)
        {
            var UsuarioDominio = unitOfWork.UsuarioDomain;
            var Collection = UsuarioDominio.GetList(_ => _.ID > 0); // Default Search

            if (!String.IsNullOrEmpty(Model.NOME))
                Collection.Where(_=>_.NOME.Contains(Model.NOME)).ToList(); 

            if (!String.IsNullOrEmpty(Model.STATUS))
                Collection.Where(_ => _.STATUS == Model.STATUS).ToList();

            return View("Index", Collection.ToPagedList(1, PageSize));
        }          
    }
}
