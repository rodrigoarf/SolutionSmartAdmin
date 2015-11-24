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
    public partial class Loja
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(LojaDto model)
        {
            _unitOfWork.LojaRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public LojaDto SaveGetItem(LojaDto model)
        {
           var retorno = _unitOfWork.LojaRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<LojaDto> model)
        {
            _unitOfWork.LojaRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(LojaDto model)
        {
            _unitOfWork.LojaRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public LojaDto GetItem(Expression<Func<LojaDto, bool>> filter)
        {
            LojaDto model;
            model = _unitOfWork.LojaRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public void Delete(Expression<Func<LojaDto, bool>> filter)
        {
            var Collection = _unitOfWork.LojaRepository.GetList(filter);
            if (Collection.Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.LojaRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public void DeleteAll(List<LojaDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.LojaRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<LojaDto> GetList(Expression<Func<LojaDto, bool>> filter)
        {
            List<LojaDto> collection;
            collection = _unitOfWork.LojaRepository.GetList(filter);
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

        ~Loja()
        {
            Clear(false);
        }
    }
}

