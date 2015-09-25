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
    public partial class MensagemEnviada
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(MensagemEnviadaDto model)
        {
            _unitOfWork.MensagemEnviadaRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public MensagemEnviadaDto SaveGetItem(MensagemEnviadaDto model)
        {
           var retorno = _unitOfWork.MensagemEnviadaRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<MensagemEnviadaDto> model)
        {
            _unitOfWork.MensagemEnviadaRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(MensagemEnviadaDto model)
        {
            _unitOfWork.MensagemEnviadaRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public MensagemEnviadaDto GetItem(Expression<Func<MensagemEnviadaDto, bool>> filter)
        {
            MensagemEnviadaDto model;
            model = _unitOfWork.MensagemEnviadaRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public void Delete(Expression<Func<MensagemEnviadaDto, bool>> filter)
        {
            var Collection = _unitOfWork.MensagemEnviadaRepository.GetList(filter);
            if (Collection.Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.MensagemEnviadaRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public void DeleteAll(List<MensagemEnviadaDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.MensagemEnviadaRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<MensagemEnviadaDto> GetList(Expression<Func<MensagemEnviadaDto, bool>> filter)
        {
            List<MensagemEnviadaDto> collection;
            collection = _unitOfWork.MensagemEnviadaRepository.GetList(filter);
            return (collection);
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

        ~MensagemEnviada()
        {
            Clear(false);
        }
    }
}

