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
    public partial class Acesso
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(AcessoDto model)
        {
            _unitOfWork.AcessoRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public AcessoDto SaveGetItem(AcessoDto model)
        {
           var retorno = _unitOfWork.AcessoRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<AcessoDto> model)
        {
            _unitOfWork.AcessoRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(AcessoDto model)
        {
            _unitOfWork.AcessoRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public AcessoDto GetItem(Expression<Func<AcessoDto, bool>> filter)
        {
            AcessoDto model;
            model = _unitOfWork.AcessoRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<AcessoDto> GetList(Expression<Func<AcessoDto, bool>> filter)
        {
            List<AcessoDto> collection;
            collection = _unitOfWork.AcessoRepository.GetList(filter);
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

        ~Acesso()
        {
            Clear(false);
        }
    }
}

