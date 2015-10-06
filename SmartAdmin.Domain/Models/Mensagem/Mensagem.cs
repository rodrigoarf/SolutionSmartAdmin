using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SmartAdmin.Data;
using SmartAdmin.Dto;

namespace SmartAdmin.Domain
{
    public partial class Mensagem
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(MensagemDto model)
        {
            _unitOfWork.MensagemRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public MensagemDto SaveGetItem(MensagemDto model)
        {
           var retorno = _unitOfWork.MensagemRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<MensagemDto> model)
        {
            _unitOfWork.MensagemRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(MensagemDto model)
        {
            _unitOfWork.MensagemRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public MensagemDto GetItem(Expression<Func<MensagemDto, bool>> filter)
        {
            MensagemDto model;
            model = _unitOfWork.MensagemRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public void Delete(Expression<Func<MensagemDto, bool>> filter)
        {
            var Collection = _unitOfWork.MensagemRepository.GetList(filter);
            if (Collection.Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.MensagemRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public void DeleteAll(List<MensagemDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.MensagemRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<MensagemDto> GetList(Expression<Func<MensagemDto, bool>> filter)
        {
            List<MensagemDto> collection;
            collection = _unitOfWork.MensagemRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Retorna lista de mensagens da caixa de entrada do usuario.
        /// </summary>
        public List<MensagemDto> GetInboxMessages(int IdUsuarioLogado)
        {   
            var _unitOfWorkMensagemDestinatarios = _unitOfWork.MensagemEnviadaRepository;
            var CollectionRemetentesDaMensagem = _unitOfWorkMensagemDestinatarios.GetList(_ => _.COD_REMETENTE == IdUsuarioLogado);

            var _unitOfWorkMensagem = _unitOfWork.MensagemRepository;
            var CollectionMensagem = _unitOfWorkMensagem.GetList(null);

            var Collection = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
                                               MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
                                               Mensagem => Mensagem.ID,
                                               (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
                                               .Select(_ => new MensagemDto
                                               {
                                                   ID = _.MensagemDto.ID,
                                                   COD_AUTOR = _.MensagemDto.COD_AUTOR,
                                                   TITULO  = _.MensagemDto.TEXTO,
                                                   TEXTO  = _.MensagemDto.TEXTO,  
                                                   DTH_CRIACAO = _.MensagemDto.DTH_CRIACAO,
                                                   DTH_ENVIO = _.MensagemDto.DTH_ENVIO                                            
                                               }).OrderByDescending(_=>_.DTH_ENVIO).ToList();

            return (Collection); 
        }

        /// <summary>
        /// Retorna lista de mensagens da caixa de enviados do usuario.
        /// </summary>
        public List<MensagemDto> GetSentMessages(int IdUsuarioLogado)
        {
            var _unitOfWorkMensagemDestinatarios = _unitOfWork.MensagemEnviadaRepository;
            var CollectionRemetentesDaMensagem = _unitOfWorkMensagemDestinatarios.GetList(_ => _.COD_AUTOR == IdUsuarioLogado);

            var _unitOfWorkMensagem = _unitOfWork.MensagemRepository;
            var CollectionMensagem = _unitOfWorkMensagem.GetList(null);

            var Collection = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
                                               MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
                                               Mensagem => Mensagem.ID,
                                               (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
                                               .Select(_ => new MensagemDto
                                               {
                                                   ID = _.MensagemDto.ID,
                                                   COD_AUTOR = _.MensagemDto.COD_AUTOR,
                                                   TITULO = _.MensagemDto.TEXTO,
                                                   TEXTO = _.MensagemDto.TEXTO,
                                                   DTH_CRIACAO = _.MensagemDto.DTH_CRIACAO,
                                                   DTH_ENVIO = _.MensagemDto.DTH_ENVIO
                                               }).OrderByDescending(_ => _.DTH_ENVIO).ToList();

            return (Collection);
        }

        /// <summary>
        /// Retorna lista de mensagens da caixa de lixo eletrônico do usuario.
        /// </summary>
        //public List<MensagemDto> GetTrashMessages(int IdUsuarioLogado)
        //{
        //    var _unitOfWorkMensagemDestinatarios = _unitOfWork.MensagemEnviadaRepository;
        //    var CollectionRemetentesDaMensagem = _unitOfWorkMensagemDestinatarios.GetList(_ => _.COD_AUTOR == IdUsuarioLogado || _.COD_REMETENTE == IdUsuarioLogado);

        //    var _unitOfWorkMensagem = _unitOfWork.MensagemRepository;
        //    var CollectionMensagem = _unitOfWorkMensagem.GetList(_ => _.STATUS == "3" && _.COD_AUTOR == IdUsuarioLogado);

        //    var Collection = CollectionRemetentesDaMensagem.Join(CollectionMensagem,
        //                                       MensagemEnviada => MensagemEnviada.COD_MENSAGEM,
        //                                       Mensagem => Mensagem.ID,
        //                                       (MensagemEnviadaDto, MensagemDto) => new { MensagemEnviadaDto, MensagemDto })
        //                                       .Select(_ => new MensagemDto
        //                                       {
        //                                           ID = _.MensagemDto.ID,
        //                                           COD_AUTOR = _.MensagemDto.COD_AUTOR,
        //                                           TITULO = _.MensagemDto.TEXTO,
        //                                           TEXTO = _.MensagemDto.TEXTO,
        //                                           DTH_CRIACAO = _.MensagemDto.DTH_CRIACAO,
        //                                           DTH_ENVIO = _.MensagemDto.DTH_ENVIO,
        //                                           STATUS = _.MensagemDto.STATUS
        //                                       }).OrderByDescending(_ => _.DTH_ENVIO).ToList();

        //    return (Collection);
        //}

        /// <summary>
        /// Retorna o usuario autor da mensagem
        /// </summary>
        public UsuarioDto GetAutorFromMessage(int IdUsuarioAutor)
        {
            var _unitOfWorkUsuario = _unitOfWork.UsuarioRepository;
            var Model = _unitOfWorkUsuario.GetItem(_ => _.ID == IdUsuarioAutor);
            return (Model);
        }

        /// <summary>
        ///  Retorna a lista de usuario a qual a mensagem foi enviada ou seja uma lista de remetentes
        /// </summary>
        public List<UsuarioDto> GetUsersSentFromMessage(int IdMensagem)
        {
            var _unitOfWorkMensagemDestinatarios = _unitOfWork.MensagemEnviadaRepository;
            var _unitOfWorkUsuarioRemetente = _unitOfWork.UsuarioRepository; 

            var CollectionRemetentesDaMensagem = _unitOfWorkMensagemDestinatarios.GetList(_ => _.COD_MENSAGEM == IdMensagem);
            var CollectionUsuariosRemetentes = new List<UsuarioDto>();

            foreach (var item in CollectionRemetentesDaMensagem)
            {
                CollectionUsuariosRemetentes.Add(_unitOfWorkUsuarioRemetente.GetItem(_ => _.ID == item.COD_REMETENTE));
            }

            return (CollectionUsuariosRemetentes);
        }

        /// <summary>
        ///  Distroe o objeto e recursos não gerenciados liberando memória
        /// </summary>
        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                    _unitOfWork.Dispose();
            }
            _disposed = true;
        }

        ~Mensagem()
        {
            Clear(false);
        }
    }
}

