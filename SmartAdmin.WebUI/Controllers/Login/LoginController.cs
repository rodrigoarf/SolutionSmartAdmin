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

namespace SmartAdmin.WebUI.Controllers
{
    public class LoginController : BaseController
    {
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
            return View(new SmartAdmin.Dto.UsuarioDto() { NOME= "Antonio Rodrigo Fernanes", EMAIL="rodrigwerewo_arf@hotmail.com", BAIRRO="Rio Acima", CEP = "1811190", CIDADE="Votorantim", NUMERO=123, ENDERECO="Av Octavio Costa",LOGIN="rodrwerigo",SENHA="rodrigo", SEXO="M"});
        }

        [HttpPost]
        public ActionResult Logar(UsuarioDto Model)
        {
            try
            {
                var unitOfWork = new SmartAdmin.Domain.UnitOfWork();
                var UsuarioLogado = unitOfWork.UsuarioDomain.Authentication(Model);

                if (UsuarioLogado != null)
                {
                    //--> Acesso
                    var AcessoDominio = unitOfWork.AcessoDomain;
                    AcessoDominio.Save(GetUserInformation(String.Empty)); 

                    //--> Session
                    var Session = new SessionManager();                       
                    Session.Start(new Administrator() { Usuario = UsuarioLogado });
                  
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

        [HttpPost]
        public ActionResult SaveForApproval(UsuarioDto Model)
        {
            try
            {
                var UsuarioDominio = unitOfWork.UsuarioDomain;
                var ModelExists = UsuarioDominio.IsExists(Model);

                if (ModelExists == null)
                {
                    Model.STATUS = "A";
                    UsuarioDominio.Save(Model);
                    TempData["Mensagem"] = String.Format("Usuário <span style='color:#10e4ea;'>{0}</span> salvo com sucesso!", Model.LOGIN);
                    return (RedirectToAction("Registro"));
                }
                else
                {
                    TempData["Mensagem"] = String.Format("Login ou E-mail informado ja inexistente no sistema, infome outro login ou e-mail!");
                    return (RedirectToAction("Registro"));
                }
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = Ex.InnerException.InnerException.Message;
                return (RedirectToAction("Registro"));
            }
        }

        [HttpPost]
        public ActionResult Remember(UsuarioDto Model)
        {
            try
            {   
                var UsuarioDominio = unitOfWork.UsuarioDomain;
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
