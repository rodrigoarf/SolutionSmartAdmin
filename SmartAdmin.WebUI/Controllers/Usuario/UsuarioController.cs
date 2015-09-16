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
