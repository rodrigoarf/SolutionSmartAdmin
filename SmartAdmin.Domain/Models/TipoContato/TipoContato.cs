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
    public partial class TipoContato
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(TipoContatoDto model)
        {
            _unitOfWork.TipoContatoRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public TipoContatoDto SaveGetItem(TipoContatoDto model)
        {
           var retorno = _unitOfWork.TipoContatoRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<TipoContatoDto> model)
        {
            _unitOfWork.TipoContatoRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(TipoContatoDto model)
        {
            _unitOfWork.TipoContatoRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public TipoContatoDto GetItem(Expression<Func<TipoContatoDto, bool>> filter)
        {
            TipoContatoDto model;
            model = _unitOfWork.TipoContatoRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<TipoContatoDto> GetList(Expression<Func<TipoContatoDto, bool>> filter)
        {
            List<TipoContatoDto> collection;
            collection = _unitOfWork.TipoContatoRepository.GetList(filter);
            return (collection);
        }
        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            TipoContatoDto model = _unitOfWork.TipoContatoRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.TipoContatoRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            TipoContatoDto model = _unitOfWork.TipoContatoRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.TipoContatoRepository.Edit(model);
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

        ~TipoContato()
        {
            Clear(false);
        }
    }
}

