using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using SmartAdmin.Domain;
using SmartAdmin.Dto;

namespace SmartAdmin.WebUI.Controllers.Inbox
{
    public class InboxController : BaseController
    {
        [AuthorizedUser]
        public ActionResult Index(int? Page)
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();

            var MensagemEnviadaDomain = new MensagemEnviada();
            var CollectionMensagemEnviada = MensagemEnviadaDomain.GetList(_ => _.COD_REMETENTE == UsuarioLogado.Usuario.ID);

            var MensagemDomain = new Mensagem();
            var CollectionMensagem = new List<MensagemDto>();

            foreach (var item in CollectionMensagemEnviada)
	        {
                 var Mensagem = MensagemDomain.GetItem(_=>_.ID == item.ID);
                 CollectionMensagem.Add(Mensagem);
	        }

            var UsuarioDomain = new Usuario();    
            var CollectionModelView = new List<SmartAdmin.WebUI.ModelView.MensagemModelView>();
            foreach (var item in CollectionMensagem)
            {
                var Model = UsuarioDomain.GetItem(_ => _.ID == item.COD_AUTOR);
                CollectionModelView.Add(new SmartAdmin.WebUI.ModelView.MensagemModelView() { Id = item.ID, Usuario=Model, DataEnvio = item.DTH_ENVIO, Folder = item.STATUS, IsRead = false, Checked = false });  
            }

            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            return View(CollectionModelView.ToPagedList(CurrentPage, PageSize));
        }   
    }
}
