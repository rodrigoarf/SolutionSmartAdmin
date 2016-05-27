using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.Data.Model;
using SmartAdmin.Domain.Model;
using SmartAdmin.Domain.Filters;
using SmartAdmin.Domain.Annotations;
using SmartAdmin.WebUI.Controllers;
using AutoMapper;

namespace SmartAdmin.WebUI.Controllers
{
    public class NoticiaCategoriaController : BaseController
    {
        private NoticiaCategoriaSpecialized NoticiaCategoriaDomain = new NoticiaCategoriaSpecialized();

        public NoticiaCategoriaController()
        {
            ViewBag.PageTitle = "Titulo aqui!";
            ViewBag.PageDescr = "Descricao aqui!";
        }

        public ActionResult Index(int? Page)
        {
            var Collection = new List<NoticiaCategoriaDto>();
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));

            if (TempData["Listagem"] != null) { Collection = TempData["Listagem"] as List<NoticiaCategoriaDto>; }
            else { Collection = NoticiaCategoriaDomain.GetList(_ => _.ID > 0).Take(10000).ToList(); }

            ViewBag.CurrentPage = CurrentPage;
            ViewBag.TotalPage = Math.Ceiling((double)Collection.Count / PageSize);

            var CollectionAnnotations = Mapper.Map<IEnumerable<NoticiaCategoriaDto>, IEnumerable<NoticiaCategoriaAnnotations>>(Collection);
            return View(CollectionAnnotations.ToPagedList(CurrentPage, PageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Int32 Id)
        {
            var ModelView = (Id > 0) ? Mapper.Map<NoticiaCategoriaAnnotations>(NoticiaCategoriaDomain.GetItem(_ => _.ID == Id)) : new NoticiaCategoriaAnnotations();
            return View(ModelView);
        }

        public ActionResult Save(NoticiaCategoriaAnnotations ModelView)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { Id = ModelView.ID });
            }
            else
            {
                var Model = Mapper.Map<NoticiaCategoriaAnnotations, NoticiaCategoriaDto>(ModelView);

                if (Model.ID == 0)
                { NoticiaCategoriaDomain.Save(Model); }
                else
                { NoticiaCategoriaDomain.Edit(Model); }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Load(NoticiaCategoriaFilter Model)
        {
            var Collection = NoticiaCategoriaDomain.GetByFilter(_ => _.ID > 0).Take(10000);

            if (Model.ID > 0) { Collection = Collection.Where(_ => _.ID == Model.ID); }

            TempData["Listagem"] = Collection.ToList();

            return RedirectToAction("Index");
        }
    }
}

