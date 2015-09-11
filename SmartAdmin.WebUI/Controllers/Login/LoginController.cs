using SmartAdmin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Domain;
using SmartAdmin.Domain.Security;
using SmartAdmin.Domain.Helpers;

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
            return View();
        }

        [HttpPost]
        public ActionResult Logar(UsuarioDto Model)
        {
            try
            {
                var unitOfWork = new SmartAdmin.Domain.UnitOfWork(); 
                var UsuarioDominio = unitOfWork.UsuarioDomain.LoginChecker(Model);

                if (UsuarioDominio != null)
                {
                    //--> Acesso
                    var AcessoDominio = unitOfWork.AcessoDomain;
                    AcessoDominio.Save(GetUserInformation(String.Empty)); 

                    //--> Cache
                    var CurrentCache = new SmartAdmin.WebUI.Infrastructure.Cache.CacheManager();
                    CurrentCache.Save(UsuarioDominio.ID.ToString(), UsuarioDominio, 120);
                  
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
        public ActionResult Save(UsuarioDto Model)
        {

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
