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
            var UsuarioDominio = new Usuario();
            var Model = UsuarioDominio.GetList(_ => _.ID > 0);              
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));

            ViewBag.Mensagem = TempData["Mensagem"] as String;
            return View(Model.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Edit(int Id)
        {
            var UsuarioDominio = new Usuario();
            var Model = UsuarioDominio.GetItem(_ => _.ID == Id);
            return View(Model);
        }
              
        [HttpPost]
        [AuthorizedUser]
        public ActionResult Load(UsuarioDto Model)
        {                                          
            var UsuarioDominio = new Usuario();
            var Collection = new List<UsuarioDto>();

            if (!String.IsNullOrEmpty(Model.NOME))
                Collection = UsuarioDominio.GetList(null).Where(_ => _.NOME.Contains(Model.NOME)).OrderBy(_ => _.NOME).ToList();

            if (!String.IsNullOrEmpty(Model.STATUS))
                Collection = UsuarioDominio.GetList(null).Where(_ => _.STATUS == Model.STATUS).OrderBy(_ => _.NOME).ToList();

            if (String.IsNullOrEmpty(Model.NOME) && (String.IsNullOrEmpty(Model.STATUS)))
                Collection = UsuarioDominio.GetList(_ => _.ID > 0).OrderBy(_ => _.NOME).ToList(); 

            return View("Index", Collection.ToPagedList(1, PageSize));     
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult Save(UsuarioDto Model)
        {
            var UsuarioDomain = new Usuario();
            var ModelCheck = UsuarioDomain.GetItem(_=>_.LOGIN == Model.LOGIN && _.EMAIL == Model.EMAIL && _.ID != Model.ID);

            if (ModelCheck != null)
            {
                TempData["Mensagem"] = string.Format("O <span style='color:#10e4ea;'>email</span> ou <span style='color:#10e4ea;'>login</span> informado ja estão cadastrados por outro usuário!");
                return (RedirectToAction("Index", "Usuario"));
            }
            else
            {
                UsuarioDomain.Edit(Model);

                TempData["Mensagem"] = string.Format("Informações <span style='color:#10e4ea;'>atualizadas</span> com sucesso!");
                return (RedirectToAction("Index", "Usuario"));
            }
        }
                
        #region Webcam methods

        [AuthorizedUser]
        public void Capture()
        {
            var Stream = Request.InputStream;
            var Dump = String.Empty;

            using (var Reader = new System.IO.StreamReader(Stream)) { Dump = Reader.ReadToEnd(); }

            var PathImage = Server.MapPath("~/test.jpg");
            System.IO.File.WriteAllBytes(PathImage, ConvertStringToBytes(Dump));
        }

        [AuthorizedUser]
        private byte[] ConvertStringToBytes(string Input)
        {
            var NumberOfBytes = (Input.Length) / 2;
            byte[] ByteArray = new byte[NumberOfBytes];
            for (int x = 0; x < NumberOfBytes; ++x) { ByteArray[x] = Convert.ToByte(Input.Substring(x * 2, 2), 16); }
            return (ByteArray);
        }

        #endregion       
    }
}
