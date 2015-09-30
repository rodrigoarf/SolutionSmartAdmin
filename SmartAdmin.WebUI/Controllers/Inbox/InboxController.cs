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
        [AuthorizedUser]
        public ActionResult Index(int? Page)
        {
            var CollectionMensagensViewModel = InboxMessages();
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            ViewBag.SetMenu = "1";

            ViewBag.Mensagem = TempData["Mensagem"] as String;
            return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Sent(int? Page)
        {
            var CollectionMensagensViewModel = SentMessages();
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            ViewBag.SetMenu = "2";

            return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Trash(int? Page)
        {
            var CollectionMensagensViewModel = TrashMessages();
            var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
            ViewBag.SetMenu = "3";

            return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        }

        [AuthorizedUser]
        public ActionResult Compose()
        {
            ViewBag.Mensagem = TempData["Mensagem"] as String;
            var Model = TempData["Model"] as SmartAdmin.WebUI.ModelView.MensagemModelView;
            return View((Model != null) ? Model : new SmartAdmin.WebUI.ModelView.MensagemModelView());
        }

        [AuthorizedUser]
        public ActionResult Reader(int Id)
        {
            var MensagemEnviadaDomain = new MensagemEnviada();                  
            var ModelMensagemEnviada = MensagemEnviadaDomain.GetItem(_ => _.COD_MENSAGEM == Id);
            ModelMensagemEnviada.STATUS = "S";
            MensagemEnviadaDomain.Edit(ModelMensagemEnviada);

            var MensagemDomain = new Mensagem();
            var ModelMensagem = MensagemDomain.GetItem(_ => _.ID == ModelMensagemEnviada.COD_MENSAGEM);

            var UsuarioDomain = new Usuario();

            var MensagemModelView = new SmartAdmin.WebUI.ModelView.MensagemModelView();
            MensagemModelView.MensagemAutor = UsuarioDomain.GetItem(_ => _.ID == ModelMensagemEnviada.COD_AUTOR).EMAIL;
            MensagemModelView.MensagemTitulo = ModelMensagem.TITULO;
            MensagemModelView.MensagemTexto = ModelMensagem.TEXTO;

            return View(MensagemModelView);
        }

        [HttpPost]
        [AuthorizedUser]
        public ActionResult Save(SmartAdmin.WebUI.ModelView.MensagemModelView ModelView)
        {
            try
            {
                var UsuarioDomain = new Usuario();
                var ModelMensagem = new MensagemDto();               
                  
                var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
                var UsuarioLogado = SessaoDomain.GetUsuario();

                ModelMensagem.COD_AUTOR = UsuarioLogado.Usuario.ID;
                ModelMensagem.TITULO = ModelView.MensagemTitulo;
                ModelMensagem.TEXTO = ModelView.MensagemTexto;
                ModelMensagem.STATUS = "2";
                ModelMensagem.DTH_CRIACAO = DateTime.Now;
                ModelMensagem.DTH_ENVIO = DateTime.Now;

                var MensagemDomain = new Mensagem();
                var MensagemSalva = MensagemDomain.SaveGetItem(ModelMensagem);

                var Destinatario = UsuarioDomain.GetItem(_ => _.EMAIL == ModelView.MensagemDestinatario.ToLower());

                if (Destinatario != null)
                {  
                    var MensagemEnviadaDomain = new MensagemEnviada();
                    var MensagemEnviadaModel = new MensagemEnviadaDto();

                    MensagemEnviadaModel.COD_MENSAGEM = MensagemSalva.ID;
                    MensagemEnviadaModel.COD_AUTOR  = UsuarioLogado.Usuario.ID;
                    MensagemEnviadaModel.COD_REMETENTE  = Destinatario.ID;
                    MensagemEnviadaModel.STATUS = "N";

                    MensagemEnviadaDomain.Save(MensagemEnviadaModel);     

                    TempData["Mensagem"] = "Mensagem enviada com sucesso!";
                    TempData["Model"] = ModelView;
                    return (RedirectToAction("Index", "Inbox"));
                } 
                else
                {
                    TempData["Mensagem"] = "E-mail de destinatário nao encontrado!";
                    return (RedirectToAction("Compose", "Inbox", ModelView));
                }
            }
            catch (Exception Ex)
            {
                TempData["Mensagem"] = "Erro ao enviar mensagem " + Ex;
                return (RedirectToAction("Index", "Inbox"));
            }
        }

        private List<MensagemModelView> InboxMessages()
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();

            var MensagemEnviadaDomain = new MensagemEnviada();
            var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

            CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_REMETENTE == UsuarioLogado.Usuario.ID);

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
                                                                        MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
                                                                    }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
            return CollectionMensagensViewModel;
        }

        private List<MensagemModelView> SentMessages()
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();

            var MensagemEnviadaDomain = new MensagemEnviada();
            var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

            CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID);

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
                                                                        MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
                                                                    }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
            return CollectionMensagensViewModel;
        }

        private List<MensagemModelView> TrashMessages()
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();

            var MensagemEnviadaDomain = new MensagemEnviada();
            var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

            CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID || _.COD_REMETENTE == UsuarioLogado.Usuario.ID);

            var MensagemDomain = new Mensagem();
            var CollectionMensagem = MensagemDomain.GetList(_ => _.STATUS == "3" && _.COD_AUTOR == UsuarioLogado.Usuario.ID);

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
                                                                        MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
                                                                    }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
            return CollectionMensagensViewModel;
        }

        private void TotalDeMensagens()
        {
            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
            var UsuarioLogado = SessaoDomain.GetUsuario();
            var MensagemEnviadaDomain = new MensagemEnviada();
            ViewBag.TotalDeMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_REMETENTE == UsuarioLogado.Usuario.ID && _.STATUS == "N").Count;
        }

        public SmartAdmin.WebUI.ModelView.EMensagemFolder RetornaStatus(string Status)
        {
            switch (Status)
            {
                case "1":
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeEntrada);
                case "2":
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeSaida);
                case "3":
                    return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeLixoEletronico);
                default:
                    goto case "1";
            }
        }
    }
}
