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
    public class NoticiaController : BaseController
    {
        private NoticiaSpecialized NoticiaDomain = new NoticiaSpecialized();

        public NoticiaController()
        {
            ViewBag.PageTitle = "Not�cias";
            ViewBag.PageDescr = "Cadastro de Not�cias";
        }

        public ActionResult Index(int? Page)
        {
            var Collection = new List<NoticiaDto>();
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));

            if (TempData["Listagem"] != null) { Collection = TempData["Listagem"] as List<NoticiaDto>; }
            else { Collection = NoticiaDomain.GetList(_ => _.ID > 0).Take(10000).ToList(); }

            ViewBag.CurrentPage = CurrentPage;
            ViewBag.TotalPage = Math.Ceiling((double)Collection.Count / PageSize);

            var CollectionAnnotations = Mapper.Map<IEnumerable<NoticiaDto>, IEnumerable<SmartAdmin.Domain.Annotations.Noticia>>(Collection);
            return View(CollectionAnnotations.ToPagedList(CurrentPage, PageSize));
        }

        public ActionResult Create()
        {
            return View(new SmartAdmin.Domain.Annotations.NoticiaAnnotations());
        }

        public ActionResult Edit(Int32 Id)
        {
            var ModelView = (Id > 0) ? Mapper.Map<NoticiaAnnotations>(NoticiaDomain.GetItem(_ => _.ID == Id)) : new NoticiaAnnotations();
            return View(ModelView);
        }

        public ActionResult Save(NoticiaAnnotations ModelView)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { Id = ModelView.ID });
            }
            else
            {
                var Model = Mapper.Map<NoticiaAnnotations, NoticiaDto>(ModelView);

                if (Model.ID == 0)
                { NoticiaDomain.Save(Model); }
                else
                { NoticiaDomain.Edit(Model); }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Load(NoticiaFilter Model)
        {
            var Collection = NoticiaDomain.GetByFilter(_ => _.ID > 0).Take(10000);

            if (Model.ID > 0) { Collection = Collection.Where(_ => _.ID == Model.ID); }

            TempData["Listagem"] = Collection.ToList();

            return RedirectToAction("Index");
        }
    }
}

