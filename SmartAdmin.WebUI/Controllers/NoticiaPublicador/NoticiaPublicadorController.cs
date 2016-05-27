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
    public class NoticiaPublicadorController : BaseController
    {
        private NoticiaPublicadorSpecialized NoticiaPublicadorDomain = new NoticiaPublicadorSpecialized();

        public NoticiaPublicadorController()
        {
            ViewBag.PageTitle = "Titulo aqui!";
            ViewBag.PageDescr = "Descricao aqui!";
        }

        public ActionResult Index(int? Page)
        {
            var Collection = new List<NoticiaPublicadorDto>();
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));

            if (TempData["Listagem"] != null) { Collection = TempData["Listagem"] as List<NoticiaPublicadorDto>; }
            else { Collection = NoticiaPublicadorDomain.GetList(_ => _.ID > 0).Take(10000).ToList(); }

            ViewBag.CurrentPage = CurrentPage;
            ViewBag.TotalPage = Math.Ceiling((double)Collection.Count / PageSize);

            var CollectionAnnotations = Mapper.Map<IEnumerable<NoticiaPublicadorDto>, IEnumerable<NoticiaPublicadorAnnotations>>(Collection);
            return View(CollectionAnnotations.ToPagedList(CurrentPage, PageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Int32 Id)
        {

            var ModelView = (Id > 0) ? Mapper.Map<NoticiaPublicadorAnnotations>(NoticiaPublicadorDomain.GetItem(_ => _.ID == Id)) : new NoticiaPublicadorAnnotations();
            return View(ModelView);
        }

        public ActionResult Save(NoticiaPublicadorAnnotations ModelView)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { Id = ModelView.ID });
            }
            else
            {
                var Model = Mapper.Map<NoticiaPublicadorAnnotations, NoticiaPublicadorDto>(ModelView);

                if (Model.ID == 0)
                { NoticiaPublicadorDomain.Save(Model); }
                else
                { NoticiaPublicadorDomain.Edit(Model); }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Load(NoticiaPublicadorFilter Model)
        {
            var Collection = NoticiaPublicadorDomain.GetByFilter(_ => _.ID > 0).Take(10000);

            if (Model.ID > 0) { Collection = Collection.Where(_ => _.ID == Model.ID); }

            TempData["Listagem"] = Collection.ToList();

            return RedirectToAction("Index");
        }
    }
}

