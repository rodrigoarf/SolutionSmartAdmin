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
    public partial class Complexidade
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto
        /// </summary>
        public void Save(ComplexidadeDto model)
        {
            _unitOfWork.ComplexidadeRepository.AddItem(model);
        }

        /// <summary>
        /// Salva e retorna um objeto
        /// </summary>
        public ComplexidadeDto SaveGetItem(ComplexidadeDto model)
        {
           var retorno = _unitOfWork.ComplexidadeRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos
        /// </summary>
        public void SaveAll(List<ComplexidadeDto> model)
        {
            _unitOfWork.ComplexidadeRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto
        /// </summary>
        public void Edit(ComplexidadeDto model)
        {
            _unitOfWork.ComplexidadeRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto buscado por expressão Lambda
        /// </summary>
        public ComplexidadeDto GetItem(Expression<Func<ComplexidadeDto, bool>> filter)
        {
            ComplexidadeDto model;
            model = _unitOfWork.ComplexidadeRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna um objeto do tipo List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<ComplexidadeDto> GetList(Expression<Func<ComplexidadeDto, bool>> filter)
        {
            List<ComplexidadeDto> collection;
            collection = _unitOfWork.ComplexidadeRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        ///  Distroe objeto e recursos não gerenciados liberando memória
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

        ~Complexidade()
        {
            Clear(false);
        }
    }
}

