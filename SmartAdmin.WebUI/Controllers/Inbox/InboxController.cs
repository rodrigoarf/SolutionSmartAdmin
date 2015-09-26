using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using SmartAdmin.Domain;
using SmartAdmin.Dto;
using SmartAdmin.WebUI.ModelView;

namespace SmartAdmin.WebUI.Controllers.Inbox
{
    public class InboxController : BaseController
    {
       
        public ActionResult Index(int? Page)
        {                                    
            var CollectionMensagensViewModel = GetMessages(EMensagemFolder.CaixaDeEntrada);  
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            TotalDeMensagens(); 

            return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        }   

        public ActionResult Sent(int? Page)
        {                                            
            var CollectionMensagensViewModel = GetMessages(EMensagemFolder.CaixaDeSaida);
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            TotalDeMensagens();

            return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        }      

        private void TotalDeMensagens()
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();
            var MensagemEnviadaDomain = new MensagemEnviada();
            ViewBag.TotalDeMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_REMETENTE == UsuarioLogado.Usuario.ID && _.STATUS == "N").Count;
        }

        private List<MensagemModelView> GetMessages(SmartAdmin.WebUI.ModelView.EMensagemFolder InFolder)
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();

            var MensagemEnviadaDomain = new MensagemEnviada();
            var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();
            if (InFolder == SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeSaida)
            {
                CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID);
            }
            else
            {
                CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_REMETENTE == UsuarioLogado.Usuario.ID);
            }
           
            var MensagemDomain = new Mensagem();
            var CollectionMensagem = MensagemDomain.GetList(null);

            var UsuarioDomain = new Usuario();
            var CollectionUsuario = UsuarioDomain.GetList(null);

            var CollectionMensagensViewModel = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
                                                                    MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
                                                                    Mensagem => Mensagem.ID,
                                                                   (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
                                                                   .Select(_ => new SmartAdmin.WebUI.ModelView.MensagemModelView
                                                                   {
                                                                       MensagemId = _.MensagemDto.ID,
                                                                       MensagemTitulo = _.MensagemDto.TITULO,
                                                                       MensagemTexto = _.MensagemDto.TEXTO,
                                                                       MensagemDataCriacao = _.MensagemDto.DTH_CRIACAO,
                                                                       MensagemDataEnvio = _.MensagemDto.DTH_ENVIO,
                                                                       MensagemLida = ((_.MensagemEnviadaDto.STATUS == "S") ? true : false),
                                                                       MensagemPasta = RetornaStatus(_.MensagemDto.STATUS),
                                                                       MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault()
                                                                   }).ToList();
            return CollectionMensagensViewModel;
        }

        public SmartAdmin.WebUI.ModelView.EMensagemFolder RetornaStatus(string Status)
        {
            switch (Status)
            {
                case "1" :
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeEntrada);
                case "2" :
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeSaida);
                case "3" :
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeImportantes);
                case "4" :
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeLixoEletronico);
                case "5" :
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeRascunhos);
                default :
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeEntrada);
            }
        }

    }
}
