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

