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
        public ActionResult Edit(Nullable<int> id)
        {
            if (id == null)
            {
                var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
                var UsuarioLogado = SessaoDomain.GetUsuario();  
                id = UsuarioLogado.Usuario.ID;
            }      

            var UsuarioDominio = new Usuario();
            var Model = UsuarioDominio.GetItem(_ => _.ID == id);   
            return View(Model);
        }
              
        [HttpPost]
        [AuthorizedUser]
        public ActionResult Load(UsuarioDto Model)
        {                                          
            var UsuarioDominio = new Usuario();
            var Collection = UsuarioDominio.GetByFilters(Model);

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

        [HttpPost]
        [AuthorizedUser]
        public ActionResult SavePermission(UsuarioDto Model)
        {
            var UsuarioDomain = new Usuario();
            var ModelCheck = UsuarioDomain.GetItem(_ => _.LOGIN == Model.LOGIN && _.EMAIL == Model.EMAIL && _.ID != Model.ID);

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

        public ActionResult SaveMenuPermission(SmartAdmin.WebUI.ModelView.PermissionModelView Model)
        { 
            var MenuUsuarioDomain = new MenuUsuario();
            MenuUsuarioDomain.Delete(_ => _.COD_USUARIO == Model.ID);

            var Collection = new List<MenuUsuarioDto>();
            foreach (var item in Model.CollectionMenus) {
                if (item.Checked == true)
                {
                    Collection.Add(new MenuUsuarioDto() { COD_MENU = item.ID, COD_USUARIO = Model.ID }); 
                } 
            }
            
            MenuUsuarioDomain.SaveAll(Collection);  
            return (RedirectToAction("Index", "Usuario"));
        }

        [AuthorizedUser]
        public PartialViewResult PermissionMenuPartial(int Id)
        {
            var UsuarioDomain = new Usuario();
            var Model = UsuarioDomain.GetItem(_ => _.ID == Id);

            var MenuDomain = new Menu();
            var Collection = MenuDomain.GetList(_=>_.STATUS=="A" && _.COD_MENU_PAI == 0);

            var MenuUsuarioDomain = new MenuUsuario();
            var CollectionAllowed = MenuUsuarioDomain.GetList(_ => _.COD_USUARIO == Id);

            var CheckboxList = new List<SmartAdmin.WebUI.ModelView.CheckBoxes>();
            bool retorno=false;

            foreach (var item in Collection)
	        {
                foreach (var itemAllowed in CollectionAllowed)
                {
                    if (item.ID == itemAllowed.COD_MENU)
                    {
                        retorno = true;
                        break;
                    }
                    else 
                    {
                        retorno = false;
                        continue;
                    }
                }

                CheckboxList.Add(new SmartAdmin.WebUI.ModelView.CheckBoxes() { ID = item.ID, Text = item.NOME, Value = item.ID.ToString(), Checked = retorno });
	        }

            var ModelView = new SmartAdmin.WebUI.ModelView.PermissionModelView();
            ModelView.ID = Model.ID;
            ModelView.NOME = Model.NOME;
            ModelView.CollectionMenus = CheckboxList;

            return (PartialView("PermissionMenuPartial", ModelView));
        }

        [HttpPost]
        public ActionResult SaveForApproval(UsuarioDto Model)
        {
            try
            {
                var UsuarioDominio = new Usuario();
                var ModelExists = UsuarioDominio.IsExistsByDocument(Model.CPF_CNPJ.Trim());

                if (ModelExists == null)
                {
                    if (CheckInitialValues(Model))
                    {
                        TempData["Mensagem"] = String.Format("O campo login deve iniciar com <span style='color:#10e4ea;'>3 letras</span>, após isso números e letras são permitidos", Model.LOGIN);
                        TempData["Model"] = Model;
                        return (RedirectToAction("Index"));
                    }
                    else if (Model.LOGIN.Length <= 7)
                    {
                        TempData["Mensagem"] = String.Format("O campo login contém apenas <span style='color:#10e4ea;'>{0} caracteres</span>, é esperado entre <span style='color:#10e4ea;'>7</span> e <span style='color:#10e4ea;'>14</span> caracteres.", Model.LOGIN.Length);
                        TempData["Model"] = Model;
                        return (RedirectToAction("Index"));
                    }

                    UsuarioDominio.Save(Model);
                    TempData["Mensagem"] = String.Format("Usuário <span style='color:#10e4ea;'>{0}</span> salvo com sucesso!", Model.LOGIN);
                    return (RedirectToAction("Index"));
                }
                else
                {
                    TempData["Mensagem"] = String.Format("<span style='color:#10e4ea;'>Documento</span> informado ja existente no sistema, informe outro Cnpj ou Cpf!");
                    TempData["Model"] = Model;
                    return (RedirectToAction("Index"));
                }
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = Ex.InnerException.InnerException.Message;
                TempData["Model"] = Model;
                return (RedirectToAction("Index"));
            }
        }

        private static bool CheckInitialValues(UsuarioDto Model)
        {
            var CheckValues = Model.LOGIN.ToString().Substring(0, 3).ToCharArray();
            var IsNumeric = false;
            int Inteiro;

            for (int i = 0; i < CheckValues.Length; i++)
            {
                IsNumeric = int.TryParse(CheckValues[i].ToString(), out Inteiro);
                if (IsNumeric) { break; }
            }

            return (IsNumeric);
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
