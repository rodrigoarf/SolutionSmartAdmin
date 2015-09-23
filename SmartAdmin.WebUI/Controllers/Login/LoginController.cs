using SmartAdmin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Domain;
using SmartAdmin.Domain.Security;
using SmartAdmin.Domain.Helpers;
using SmartAdmin.WebUI.Infrastructure.Session;
using SmartAdmin.WebUI.ModelView;
using System.Web.Security;

namespace SmartAdmin.WebUI.Controllers
{
    public class LoginController : BaseController
    {
        private Usuario UsuarioDomain { get; set; }

        public LoginController()
        {
            UsuarioDomain = new Usuario();
        }

        public ActionResult Index()
        {
            var t = new SmartAdmin.Domain.Security.Cryptography();
            ViewBag.Pass = t.Encrypt("rodrigo");

            ViewBag.Mensagem = ((TempData["Mensagem"] != null) ? TempData["Mensagem"] as String : null);
            return View();
        }

        public ActionResult Registro()
        {
            ViewBag.Mensagem = TempData["Mensagem"] as String;
            var Model = TempData["Model"] as UsuarioDto;
            return View((Model == null) ? new SmartAdmin.Dto.UsuarioDto() : Model);
        }

        [HttpPost]
        public ActionResult Logar(UsuarioDto Model, string Remind)
        {
            try
            {
                var UsuarioLogado = UsuarioDomain.Authentication(Model);

                if (UsuarioLogado != null)
                {
                    //--> Acesso
                    var AcessoDomain = new SmartAdmin.Domain.Acesso();
                    AcessoDomain.Save(GetUserInformation(String.Empty));

                    //--> Menus & Submenus
                    var CollectionMenuMain = new List<MenuModelView>();                    
                    foreach (var MenuMain in UsuarioDomain.GetAllowedMenus(UsuarioLogado.ID)) //--> Para cada menu pai pega os filhos e adiciona no modelo de visão
                    {
                       var CollectionSubMenus = UsuarioDomain.GetSubMenuFromMenu(MenuMain.ID);
                       var CurrentMenuMain = new MenuModelView() { Menu = MenuMain, CollectionSubMenu = CollectionSubMenus }; 
                       CollectionMenuMain.Add(CurrentMenuMain);
                    }

                    //--> Session
                    var Session = new SessionManager();
                    Session.Start(new UsuarioModelView() { Usuario = UsuarioLogado, CollectionMenusAndSubMenus = CollectionMenuMain });

                    //--> Cookie
                    CreateCookie(UsuarioLogado, Remind);
                    //--> Cookie
                  
                    return (RedirectToAction("Index", "Menu"));
                }
                else
                {
                    TempData["Mensagem"] = "Usuário inexistente ou não esta ativo no sistema, contate o Administrador!";
                    return (RedirectToAction("Index", "Login"));
                }  
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        private void CreateCookie(UsuarioDto UsuarioLogado, string Remind)
        {
            var AuthTicket = new FormsAuthenticationTicket(
                 1,
                 UsuarioLogado.ID.ToString(),     //User Id
                 DateTime.Now,                    //Initial Time 
                 DateTime.Now.AddDays(15),        //Expiry Time 
                 Convert.ToBoolean(Remind),       //True to Remember                      
                 string.Empty,                    //Roles if exists 
                 "/"                              //Path of Cookie 
               );

            HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(AuthTicket));
            Response.Cookies.Add(Cookie);
        }

        public ActionResult SessionUpdateObject()
        {
            var Session = new SessionManager();
            var ModelView = Session.GetUsuario();

            //--> Atualiza Dados do Usuário
            var UsuarioDomain = new Usuario();
            var UsuarioLogado = UsuarioDomain.GetItem(_ => _.ID == ModelView.Usuario.ID);
                
            //--> Atualiza Menus & Submenus
            var CollectionMenuMain = new List<MenuModelView>();
            foreach (var MenuMain in UsuarioDomain.GetAllowedMenus(ModelView.Usuario.ID)) //--> Para cada menu pai pega os filhos e adiciona no modelo de visão
            {
                var CollectionSubMenus = UsuarioDomain.GetSubMenuFromMenu(MenuMain.ID);
                var CurrentMenuMain = new MenuModelView() { Menu = MenuMain, CollectionSubMenu = CollectionSubMenus };
                CollectionMenuMain.Add(CurrentMenuMain);
            }
            var Id = ModelView.Usuario.ID;
            ModelView = null;
            Session.Start(new UsuarioModelView() { Usuario = UsuarioLogado, CollectionMenusAndSubMenus = CollectionMenuMain });

            return (RedirectToAction("Edit", "Usuario", new { Id = Id }));
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
                        return (RedirectToAction("Registro"));
                    }
                    else if (Model.LOGIN.Length <= 7)
                    {
                        TempData["Mensagem"] = String.Format("O campo login contém apenas <span style='color:#10e4ea;'>{0} caracteres</span>, é esperado entre <span style='color:#10e4ea;'>7</span> e <span style='color:#10e4ea;'>14</span> caracteres.", Model.LOGIN.Length);
                        TempData["Model"] = Model;
                        return (RedirectToAction("Registro"));
                    }
                    
                    UsuarioDominio.Save(Model);
                    TempData["Mensagem"] = String.Format("Usuário <span style='color:#10e4ea;'>{0}</span> salvo com sucesso!", Model.LOGIN);
                    return (RedirectToAction("Registro"));
                }
                else
                {
                    TempData["Mensagem"] = String.Format("<span style='color:#10e4ea;'>Documento</span> informado ja existente no sistema, informe outro Cnpj ou Cpf!");
                    TempData["Model"] = Model;
                    return (RedirectToAction("Registro"));
                }
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = Ex.InnerException.InnerException.Message;
                TempData["Model"] = Model;
                return (RedirectToAction("Registro"));
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

        [HttpPost]
        public ActionResult Remember(UsuarioDto Model)
        {
            try
            {   
                var UsuarioDominio = new Usuario();
                var ModelExists = UsuarioDominio.IsExists(Model);

                if (ModelExists != null)
                {
                    var NewPass = SmartAdmin.Domain.Helpers.Untils.GeneratePassword(14);
                    var Crypt = new Cryptography();

                    ModelExists.SENHA = Crypt.Encrypt(NewPass);
                    UsuarioDominio.Edit(ModelExists);

                    var Email = new Email("rodrigo_arf@hotmail.com", "rodrigo_arf@hotmail.com", "Recuperação de Senha", "bla...bla...bla...bla...bla...");
                    if (Email.Enviar())
                        TempData["Mensagem"] = "Uma nova senha foi enviada para o e-email cadastrado. Por favor verifique sua caixa de e-mails!";

                }
                else
                {
                    TempData["Mensagem"] = "Login e E-mail não encontrados no sistema, não foi possivel enviar uma nova senha!";
                }
                return (RedirectToAction("Index", "Login"));
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = Ex.Message;
                return (RedirectToAction("Index", "Login"));
            }
        }             

    }
}
