using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SmartAdmin.WebUI.Infrastructure.ActionFilters;
using SmartAdmin.Domain;
using SmartAdmin.Data.Model;
using SmartAdmin.WebUI.ModelView;

namespace SmartAdmin.WebUI.Controllers.Inbox
{
    public class InboxController : BaseController
    {
        //[AuthorizedUser]
        //public ActionResult Index(int? Page)
        //{
        //    var CollectionMensagensViewModel = InboxMessages();
        //    var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
        //    ViewBag.SetMenu = "1";

        //    ViewBag.Mensagem = TempData["Mensagem"] as String;
        //    return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        //}

        //[AuthorizedUser]
        //public ActionResult Sent(int? Page)
        //{
        //    var CollectionMensagensViewModel = SentMessages();
        //    var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
        //    ViewBag.SetMenu = "2";

        //    return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        //}

        //[AuthorizedUser]
        //public ActionResult Trash(int? Page)
        //{
        //    var CollectionMensagensViewModel = TrashMessages();
        //    var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
        //    ViewBag.SetMenu = "3";

        //    return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        //}

        //[AuthorizedUser]
        //public ActionResult Favorites(int? Page)
        //{
        //    var CollectionMensagensViewModel = FavoritesMessages();
        //    var CurrentPage = ((Page == null) ? 1 : Convert.ToInt32(Page));
        //    ViewBag.SetMenu = "4";

        //    ViewBag.Mensagem = TempData["Mensagem"] as String;
        //    return View(CollectionMensagensViewModel.ToPagedList(CurrentPage, PageSize));
        //}

        //[AuthorizedUser]
        //public ActionResult Compose()
        //{
        //    ViewBag.Mensagem = TempData["Mensagem"] as String;
        //    var Model = TempData["Model"] as SmartAdmin.WebUI.ModelView.MensagemModelView;
        //    return View((Model != null) ? Model : new SmartAdmin.WebUI.ModelView.MensagemModelView());
        //}

        //[AuthorizedUser]
        //public ActionResult Reader(int Id)
        //{
        //    var MensagemDomain = new MensagemSpecialized();
        //    var ModelMensagem = MensagemDomain.GetItem(_ => _.ID == Id);  
                                                                                   
        //    var MensagemModelView = new SmartAdmin.WebUI.ModelView.MensagemModelView();
        //    var UsuarioDomain = new Usuario();

        //    MensagemModelView.MensagemDestinatarioObject = MensagemDomain.GetAutorFromMessage(ModelMensagem.COD_AUTOR);
        //    MensagemModelView.CollectionDestinatarios = MensagemDomain.GetUsersSentFromMessage(ModelMensagem.ID);
        //    MensagemModelView.MensagemTitulo = ModelMensagem.TITULO;
        //    MensagemModelView.MensagemTexto = ModelMensagem.TEXTO;
        //    MensagemModelView.MensagemDataEnvio = ModelMensagem.DTH_ENVIO; 

        //    return View(MensagemModelView);
        //}

        //[HttpPost]
        //[AuthorizedUser]
        //[ValidateInput(false)]
        //public ActionResult Save(SmartAdmin.WebUI.ModelView.MensagemModelView ModelView)
        //{
        //    try
        //    {
        //        var UsuarioDomain = new Usuario();
        //        var Destinatario = UsuarioDomain.GetItem(_ => _.EMAIL == ModelView.MensagemDestinatario.ToLower());

        //        if (Destinatario != null)
        //        {
        //            var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
        //            var UsuarioLogado = SessaoDomain.GetUsuario();
        //            var ModelMensagem = new MensagemDto();

        //            //--> Salva Mensagem
        //            ModelMensagem.COD_AUTOR = UsuarioLogado.Usuario.ID;
        //            ModelMensagem.TITULO = ModelView.MensagemTitulo;
        //            ModelMensagem.TEXTO = ModelView.MensagemTexto;
        //            ModelMensagem.DTH_CRIACAO = DateTime.Now;
        //            ModelMensagem.DTH_ENVIO = DateTime.Now;

        //            var MensagemDomain = new MensagemSpecialized();
        //            var MensagemSalva = MensagemDomain.SaveGetItem(ModelMensagem);
        //            //-->
                           
        //            var MensagemEnviadaDomain = new MensagemEnviada();
        //            var MensagemEnviadaModel = new MensagemEnviadaDto();

        //            //--> Salva na tabela de enviados para cada autor
        //            MensagemEnviadaModel.COD_MENSAGEM = MensagemSalva.ID;
        //            MensagemEnviadaModel.COD_AUTOR  = UsuarioLogado.Usuario.ID;
        //            MensagemEnviadaModel.COD_REMETENTE  = Destinatario.ID;
        //            MensagemEnviadaModel.STATUS_AUTOR = "2";
        //            MensagemEnviadaModel.STATUS_REMETENTE = "1";
        //            MensagemEnviadaDomain.Save(MensagemEnviadaModel);
        //            //-->

        //            TempData["Mensagem"] = "Mensagem <span style='color:#10e4ea;'>enviada</span> com sucesso!";
        //            return (RedirectToAction("Index", "Inbox"));
        //        } 
        //        else
        //        {
        //            TempData["Mensagem"] = "E-mail de destinatário nao <span style='color:#10e4ea;'>encontrado</span>!";
        //            TempData["Model"] = ModelView;
        //            return (RedirectToAction("Compose", "Inbox"));
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        TempData["Mensagem"] = "Erro ao enviar mensagem " + Ex;
        //        return (RedirectToAction("Index", "Inbox"));
        //    }
        //}

        //[HttpPost]
        //[AuthorizedUser]
        //public ActionResult Delete(int[] values, string auxiliar)
        //{
        //    var MensagemEnviadaDomain = new MensagemEnviada();

        //    foreach (var item in values)
        //    {
        //        if (auxiliar == "1") // Caixa de Entrada
        //        {
        //            var Model = MensagemEnviadaDomain.GetItem(_ => _.COD_MENSAGEM == item);
        //            MensagemEnviadaDomain.Edit(new MensagemEnviadaDto()
        //            {
        //                COD_MENSAGEM = Model.COD_MENSAGEM,
        //                COD_AUTOR = Model.COD_AUTOR,
        //                COD_REMETENTE = Model.COD_REMETENTE,
        //                ID = Model.ID,
        //                STATUS_AUTOR = Model.STATUS_AUTOR,
        //                STATUS_REMETENTE = "3",
        //            });
        //        }

        //        if (auxiliar == "2") // Caixa de Enviados
        //        {
        //            var Model = MensagemEnviadaDomain.GetItem(_ => _.COD_MENSAGEM == item);
        //            MensagemEnviadaDomain.Edit(new MensagemEnviadaDto()
        //            {
        //                COD_MENSAGEM = Model.COD_MENSAGEM,
        //                COD_AUTOR = Model.COD_AUTOR,
        //                COD_REMETENTE = Model.COD_REMETENTE,
        //                ID = Model.ID,
        //                STATUS_AUTOR = "3",
        //                STATUS_REMETENTE = Model.STATUS_REMETENTE,
        //            });
        //        }
        //    }

        //    TempData["Mensagem"] = "O(s) iten(s) <span style='color:#10e4ea;'>selecionado(s)</span> foram envida(s) para <span style='color:#10e4ea;'>lixeira</span>.";
        //    return (RedirectToAction("Index", "Inbox"));
        //}

        //[HttpPost]
        //[AuthorizedUser]
        //public ActionResult DeleteDefinitive(int[] values, string auxiliar)
        //{
        //    if ((values != null) && (auxiliar != "3"))
        //    {
        //        var MensagemEnviadaDomain = new MensagemEnviada();
        //        foreach (var item in values)
        //        {
        //            if (auxiliar == "1") // Caixa de Entrada
        //            {
        //                var Model = MensagemEnviadaDomain.GetItem(_ => _.COD_MENSAGEM == item);
        //                MensagemEnviadaDomain.Edit(new MensagemEnviadaDto()
        //                {
        //                    COD_MENSAGEM = Model.COD_MENSAGEM,
        //                    COD_AUTOR = Model.COD_AUTOR,
        //                    COD_REMETENTE = Model.COD_REMETENTE,
        //                    ID = Model.ID,
        //                    STATUS_AUTOR = Model.STATUS_AUTOR,
        //                    STATUS_REMETENTE = "E",
        //                });
        //            }

        //            if (auxiliar == "2") // Caixa de Enviados
        //            {
        //                var Model = MensagemEnviadaDomain.GetItem(_ => _.COD_MENSAGEM == item);
        //                MensagemEnviadaDomain.Edit(new MensagemEnviadaDto()
        //                {
        //                    COD_MENSAGEM = Model.COD_MENSAGEM,
        //                    COD_AUTOR = Model.COD_AUTOR,
        //                    COD_REMETENTE = Model.COD_REMETENTE,
        //                    ID = Model.ID,
        //                    STATUS_AUTOR = "E",
        //                    STATUS_REMETENTE = Model.STATUS_REMETENTE,
        //                });
        //            }
        //        }
        //    }
            
        //    if (auxiliar == "3") // Caixa de Lixeira
        //    {
        //        var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
        //        var UsuarioLogado = SessaoDomain.GetUsuario();

        //        var MensagemEnviadaDomain = new MensagemEnviada();
        //        var CollectionTrash = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID && _.STATUS_AUTOR == "3" || _.COD_REMETENTE == UsuarioLogado.Usuario.ID && _.STATUS_REMETENTE == "3");

        //        foreach (var itemTrash in CollectionTrash)
        //        {
        //            if (itemTrash.COD_AUTOR == UsuarioLogado.Usuario.ID)
        //            {
        //                itemTrash.STATUS_AUTOR = "E";
        //            }

        //            if (itemTrash.COD_REMETENTE == UsuarioLogado.Usuario.ID)
        //            {
        //                itemTrash.STATUS_REMETENTE = "E";
        //            }

        //            MensagemEnviadaDomain.Edit(itemTrash);
        //        }
        //    }

        //    TempData["Mensagem"] = "O(s) iten(s) <span style='color:#10e4ea;'>selecionado(s)</span> foram <span style='color:#10e4ea;'>excluído(s)</span> definitivamente.";
        //    return (RedirectToAction("Index", "Inbox"));
        //}

        //private SmartAdmin.WebUI.ModelView.EMensagemFolder RetornaStatus(string Status)
        //{
        //    switch (Status)
        //    {
        //        case "1":
        //            return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeEntrada);
        //        case "2":
        //            return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeSaida);
        //        case "3":
        //            return (SmartAdmin.WebUI.ModelView.EMensagemFolder.CaixaDeLixoEletronico);
        //        default:
        //            goto case "1";
        //    }
        //}
                
        //#region Private Methods

        //private List<MensagemModelView> InboxMessages()
        //{
        //    var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
        //    var UsuarioLogado = SessaoDomain.GetUsuario();

        //    var MensagemEnviadaDomain = new MensagemEnviada();
        //    var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

        //    CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_REMETENTE == UsuarioLogado.Usuario.ID && _.STATUS_REMETENTE == "1");

        //    var MensagemDomain = new MensagemSpecialized();
        //    var CollectionMensagem = MensagemDomain.GetList(null);

        //    var UsuarioDomain = new Usuario();
        //    var CollectionUsuario = UsuarioDomain.GetList(null);

        //    var CollectionMensagensViewModel = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
        //                                                                MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
        //                                                                Mensagem => Mensagem.ID,
        //                                                                (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
        //                                                                .Select(_ => new SmartAdmin.WebUI.ModelView.MensagemModelView
        //                                                                {
        //                                                                    MensagemId = _.MensagemDto.ID,
        //                                                                    MensagemTitulo = _.MensagemDto.TITULO,
        //                                                                    MensagemTexto = _.MensagemDto.TEXTO,
        //                                                                    MensagemDataCriacao = _.MensagemDto.DTH_CRIACAO,
        //                                                                    MensagemDataEnvio = _.MensagemDto.DTH_ENVIO,
        //                                                                    MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
        //                                                                }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
        //    return CollectionMensagensViewModel;
        //}

        //private List<MensagemModelView> SentMessages()
        //{
        //    var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
        //    var UsuarioLogado = SessaoDomain.GetUsuario();

        //    var MensagemEnviadaDomain = new MensagemEnviada();
        //    var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

        //    CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID && _.STATUS_AUTOR == "2");

        //    var MensagemDomain = new MensagemSpecialized();
        //    var CollectionMensagem = MensagemDomain.GetList(null);

        //    var UsuarioDomain = new Usuario();
        //    var CollectionUsuario = UsuarioDomain.GetList(null);

        //    var CollectionMensagensViewModel = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
        //                                                            MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
        //                                                            Mensagem => Mensagem.ID,
        //                                                            (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
        //                                                            .Select(_ => new SmartAdmin.WebUI.ModelView.MensagemModelView
        //                                                            {
        //                                                                MensagemId = _.MensagemDto.ID,
        //                                                                MensagemTitulo = _.MensagemDto.TITULO,
        //                                                                MensagemTexto = _.MensagemDto.TEXTO,
        //                                                                MensagemDataCriacao = _.MensagemDto.DTH_CRIACAO,
        //                                                                MensagemDataEnvio = _.MensagemDto.DTH_ENVIO,
        //                                                                MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
        //                                                            }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
        //    return CollectionMensagensViewModel;
        //}

        //private List<MensagemModelView> TrashMessages()
        //{
        //    var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
        //    var UsuarioLogado = SessaoDomain.GetUsuario();

        //    var MensagemEnviadaDomain = new MensagemEnviada();
        //    var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

        //    CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID && _.STATUS_AUTOR == "3" || _.COD_REMETENTE == UsuarioLogado.Usuario.ID && _.STATUS_REMETENTE == "3");

        //    var MensagemDomain = new MensagemSpecialized();
        //    var CollectionMensagem = MensagemDomain.GetList(null);

        //    var UsuarioDomain = new Usuario();
        //    var CollectionUsuario = UsuarioDomain.GetList(null);

        //    var CollectionMensagensViewModel = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
        //                                                            MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
        //                                                            Mensagem => Mensagem.ID,
        //                                                            (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
        //                                                            .Select(_ => new SmartAdmin.WebUI.ModelView.MensagemModelView
        //                                                            {
        //                                                                MensagemId = _.MensagemDto.ID,
        //                                                                MensagemTitulo = _.MensagemDto.TITULO,
        //                                                                MensagemTexto = _.MensagemDto.TEXTO,
        //                                                                MensagemDataCriacao = _.MensagemDto.DTH_CRIACAO,
        //                                                                MensagemDataEnvio = _.MensagemDto.DTH_ENVIO,
        //                                                                MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
        //                                                            }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
        //    return CollectionMensagensViewModel;
        //}

        //private List<MensagemModelView> FavoritesMessages()
        //{
        //    var SessaoDomain = new SmartAdmin.WebUI.Infrastructure.Session.SessionManager();
        //    var UsuarioLogado = SessaoDomain.GetUsuario();

        //    var MensagemEnviadaDomain = new MensagemEnviada();
        //    var CollectionRemetentesDaMensagem = new List<MensagemEnviadaDto>();

        //    CollectionRemetentesDaMensagem = MensagemEnviadaDomain.GetList(_ => _.COD_AUTOR == UsuarioLogado.Usuario.ID || _.COD_REMETENTE == UsuarioLogado.Usuario.ID && _.STATUS_REMETENTE == "4");

        //    var MensagemDomain = new MensagemSpecialized();
        //    var CollectionMensagem = MensagemDomain.GetList(null);

        //    var UsuarioDomain = new Usuario();
        //    var CollectionUsuario = UsuarioDomain.GetList(null);

        //    var CollectionMensagensViewModel = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
        //                                                            MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
        //                                                            Mensagem => Mensagem.ID,
        //                                                            (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
        //                                                            .Select(_ => new SmartAdmin.WebUI.ModelView.MensagemModelView
        //                                                            {
        //                                                                MensagemId = _.MensagemDto.ID,
        //                                                                MensagemTitulo = _.MensagemDto.TITULO,
        //                                                                MensagemTexto = _.MensagemDto.TEXTO,
        //                                                                MensagemDataCriacao = _.MensagemDto.DTH_CRIACAO,
        //                                                                MensagemDataEnvio = _.MensagemDto.DTH_ENVIO,
        //                                                                MensagemAutor = CollectionUsuario.Where(thisItemCollection => thisItemCollection.ID == _.MensagemDto.COD_AUTOR).FirstOrDefault().EMAIL
        //                                                            }).OrderByDescending(_ => _.MensagemDataEnvio).ToList();
        //    return CollectionMensagensViewModel;
        //}

        //#endregion 
    }
}
