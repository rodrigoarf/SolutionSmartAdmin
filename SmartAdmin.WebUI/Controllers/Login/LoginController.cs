using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmartAdmin.Domain.Model;
using SmartAdmin.Domain.Security;
using SmartAdmin.Domain.Helpers;
using SmartAdmin.WebUI.Infrastructure.Session;
using SmartAdmin.WebUI.ModelView;
using SmartAdmin.Data.Model;      

namespace SmartAdmin.WebUI.Controllers
{
    public class LoginController : BaseController
    {
        private UsuarioSpecialized UsuarioDomain { get; set; }

        public LoginController()
        {
            UsuarioDomain = new UsuarioSpecialized();
        }

        public ActionResult Index()
        {
            ViewBag.Mensagem = ((TempData["Mensagem"] != null) ? TempData["Mensagem"] as String : null);
            return View();
        }

        public ActionResult Registro()
        {                      
            ViewBag.Mensagem = TempData["Mensagem"] as String;
            var Model = TempData["Model"] as UsuarioDto;
            return View((Model == null) ? new SmartAdmin.Data.Model.UsuarioDto() : Model);
        }

        [HttpPost]
        public ActionResult Logar(UsuarioDto Model, string Remind)
        {
            try
            {
                //var TextCrypt = new SmartAdmin.Domain.Security.Cryptography();
                //var SenhaCrypt = TextCrypt.Encrypt(Model.SENHA);  
                var UsuarioLogado = UsuarioDomain.GetItem(_ => _.LOGIN == Model.LOGIN && _.SENHA == Model.SENHA && _.STATUS == "A");

                if (UsuarioLogado != null)
                {
                    //--> Acesso
                    var AcessoDomain = new AcessoSpecialized();
                    AcessoDomain.Save(GetUserInformation(String.Empty));

                    //--> Menus & Submenus
                    var CollectionMenuMain = new List<MenuModelView>();
                    foreach (var MenuMain in UsuarioDomain.GetAllowedMenus(UsuarioLogado.ID)) //--> Para cada menu pai pega os filhos e adiciona no modelo de visão
                    {
                        var CollectionSubMenus = UsuarioDomain.GetAllowedMenusChild(MenuMain.ID);
                        var CurrentMenuMain = new MenuModelView() { Menu = MenuMain, CollectionSubMenu = CollectionSubMenus };
                        CollectionMenuMain.Add(CurrentMenuMain);
                    }

                    //--> Session
                    var Session = new SessionManager();
                    Session.Start(new UsuarioModelView() { Usuario = UsuarioLogado, CollectionMenusAndSubMenus = CollectionMenuMain });

                    return (RedirectToAction("Index", "DashBoard"));
                }
                else
                {
                    TempData["Mensagem"] = "Usuário inexistente ou não esta ativo no sistema, contate o <span style='color:#10e4ea;'>Administrador</span>";
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
            var ModelView = (UsuarioModelView)Session.GetObjectFromSession();

            //--> Atualiza Dados do Usuário
            var UsuarioDomain = new UsuarioSpecialized();
            var UsuarioLogado = UsuarioDomain.GetItem(_ => _.ID == ModelView.Usuario.ID);
                
            //--> Atualiza Menus & Submenus
            var CollectionMenuMain = new List<MenuModelView>();
           
            foreach (var MenuMain in UsuarioDomain.GetAllowedMenus(ModelView.Usuario.ID)) //--> Para cada menu pai pega os filhos e adiciona no modelo de visão
            {
                var CollectionSubMenus = UsuarioDomain.GetAllowedMenusChild(MenuMain.ID);
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
                var UsuarioDominio = new UsuarioSpecialized();
                var ModelExists = UsuarioDominio.GetItem(_ => _.CPF_CNPJ == Model.CPF_CNPJ.Trim());

                if (ModelExists != null)
                {
                    TempData["Mensagem"] = String.Format("<span style='color:#10e4ea;'>Documento Cpf/Cnpj</span> informado ja existente no sistema, informe outro Cnpj ou Cpf!");
                    TempData["Model"] = Model;
                    return (RedirectToAction("Registro"));
                }   
                else
                {
                    if (UsuarioDominio.CheckingInitialCharacter(Model))
                    {
                        TempData["Mensagem"] = String.Format("O campo login deve iniciar com <span style='color:#10e4ea;'>3 letras</span>, após isso números e letras são permitidos", Model.LOGIN);
                        TempData["Model"] = Model;
                        return (RedirectToAction("Registro"));
                    }
                    
                    if (UsuarioDominio.CheckingLoginCharacters(Model))
                    {
                        TempData["Mensagem"] = String.Format("O campo login deve conter de <span style='color:#10e4ea;'>7</span> à <span style='color:#10e4ea;'>14</span> caracteres.");
                        TempData["Model"] = Model;
                        return (RedirectToAction("Registro"));
                    }

                    //var Crypt = new SmartAdmin.Domain.Security.Cryptography();
                    //Model.SENHA = Crypt.Encrypt(Model.SENHA);

                    UsuarioDominio.Save(Model);
                    TempData["Mensagem"] = String.Format("Usuário <span style='color:#10e4ea;'>{0}</span> salvo com sucesso!", Model.LOGIN);
                    return (RedirectToAction("Index"));
                }
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = Ex.InnerException.InnerException.Message;
                TempData["Model"] = Model;
                return (RedirectToAction("Registro"));
            }
        }

        [HttpPost]
        public ActionResult Remember(UsuarioDto Model)
        {
            try
            {   
                var UsuarioDominio = new UsuarioSpecialized();
                var ModelExists = UsuarioDominio.GetByFilter(_=> _.CPF_CNPJ == Model.CPF_CNPJ).FirstOrDefault();

                if (ModelExists != null)
                {
                    var NewPass = SmartAdmin.Domain.Helpers.Untils.GeneratePassword(14);
                    var Crypt = new Cryptography();

                    ModelExists.SENHA = Crypt.Encrypt(NewPass);
                    UsuarioDominio.Edit(ModelExists);

                    var Email = new Email("rodrigo_arf@hotmail.com", "rodrigo_arf@hotmail.com", "Recuperação de Senha", "bla...bla...bla...bla...bla...");
                    if (Email.Enviar()) { TempData["Mensagem"] = "Uma nova senha foi enviada para o e-email cadastrado. Por favor verifique sua caixa de e-mails!"; }
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
