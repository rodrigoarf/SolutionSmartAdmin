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
    public partial class TipoComplexidade
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto
        /// </summary>
        public void Save(TipoComplexidadeDto model)
        {
            _unitOfWork.TipoComplexidadeRepository.AddItem(model);
        }

        /// <summary>
        /// Salva e retorna um objeto
        /// </summary>
        public TipoComplexidadeDto SaveGetItem(TipoComplexidadeDto model)
        {
           var retorno = _unitOfWork.TipoComplexidadeRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos
        /// </summary>
        public void SaveAll(List<TipoComplexidadeDto> model)
        {
            _unitOfWork.TipoComplexidadeRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto
        /// </summary>
        public void Edit(TipoComplexidadeDto model)
        {
            _unitOfWork.TipoComplexidadeRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto buscado por expressão Lambda
        /// </summary>
        public TipoComplexidadeDto GetItem(Expression<Func<TipoComplexidadeDto, bool>> filter)
        {
            TipoComplexidadeDto model;
            model = _unitOfWork.TipoComplexidadeRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna um objeto do tipo List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<TipoComplexidadeDto> GetList(Expression<Func<TipoComplexidadeDto, bool>> filter)
        {
            List<TipoComplexidadeDto> collection;
            collection = _unitOfWork.TipoComplexidadeRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            TipoComplexidadeDto model = _unitOfWork.TipoComplexidadeRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.TipoComplexidadeRepository.Edit(model);
        }

        /// <summary>
        /// Anativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            TipoComplexidadeDto model = _unitOfWork.TipoComplexidadeRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.TipoComplexidadeRepository.Edit(model);
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

        ~TipoComplexidade()
        {
            Clear(false);
        }
    }
}

