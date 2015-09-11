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
    public partial class EmailNewletter
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(EmailNewletterDto model)
        {
            _unitOfWork.EmailNewletterRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public EmailNewletterDto SaveGetItem(EmailNewletterDto model)
        {
           var retorno = _unitOfWork.EmailNewletterRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<EmailNewletterDto> model)
        {
            _unitOfWork.EmailNewletterRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(EmailNewletterDto model)
        {
            _unitOfWork.EmailNewletterRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public EmailNewletterDto GetItem(Expression<Func<EmailNewletterDto, bool>> filter)
        {
            EmailNewletterDto model;
            model = _unitOfWork.EmailNewletterRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<EmailNewletterDto> GetList(Expression<Func<EmailNewletterDto, bool>> filter)
        {
            List<EmailNewletterDto> collection;
            collection = _unitOfWork.EmailNewletterRepository.GetList(filter);
            return (collection);
        }
        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            EmailNewletterDto model = _unitOfWork.EmailNewletterRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.EmailNewletterRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            EmailNewletterDto model = _unitOfWork.EmailNewletterRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.EmailNewletterRepository.Edit(model);
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

        ~EmailNewletter()
        {
            Clear(false);
        }
    }
}

