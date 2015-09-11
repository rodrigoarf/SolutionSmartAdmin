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
    public partial class TipoNewletter
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(TipoNewletterDto model)
        {
            _unitOfWork.TipoNewletterRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public TipoNewletterDto SaveGetItem(TipoNewletterDto model)
        {
           var retorno = _unitOfWork.TipoNewletterRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<TipoNewletterDto> model)
        {
            _unitOfWork.TipoNewletterRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(TipoNewletterDto model)
        {
            _unitOfWork.TipoNewletterRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public TipoNewletterDto GetItem(Expression<Func<TipoNewletterDto, bool>> filter)
        {
            TipoNewletterDto model;
            model = _unitOfWork.TipoNewletterRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<TipoNewletterDto> GetList(Expression<Func<TipoNewletterDto, bool>> filter)
        {
            List<TipoNewletterDto> collection;
            collection = _unitOfWork.TipoNewletterRepository.GetList(filter);
            return (collection);
        }
        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            TipoNewletterDto model = _unitOfWork.TipoNewletterRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.TipoNewletterRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            TipoNewletterDto model = _unitOfWork.TipoNewletterRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.TipoNewletterRepository.Edit(model);
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

        ~TipoNewletter()
        {
            Clear(false);
        }
    }
}

