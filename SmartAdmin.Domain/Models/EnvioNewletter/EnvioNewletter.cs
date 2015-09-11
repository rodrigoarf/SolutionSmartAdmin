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
    public partial class EnvioNewletter
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(EnvioNewletterDto model)
        {
            _unitOfWork.EnvioNewletterRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public EnvioNewletterDto SaveGetItem(EnvioNewletterDto model)
        {
           var retorno = _unitOfWork.EnvioNewletterRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<EnvioNewletterDto> model)
        {
            _unitOfWork.EnvioNewletterRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(EnvioNewletterDto model)
        {
            _unitOfWork.EnvioNewletterRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public EnvioNewletterDto GetItem(Expression<Func<EnvioNewletterDto, bool>> filter)
        {
            EnvioNewletterDto model;
            model = _unitOfWork.EnvioNewletterRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<EnvioNewletterDto> GetList(Expression<Func<EnvioNewletterDto, bool>> filter)
        {
            List<EnvioNewletterDto> collection;
            collection = _unitOfWork.EnvioNewletterRepository.GetList(filter);
            return (collection);
        }
        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            EnvioNewletterDto model = _unitOfWork.EnvioNewletterRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.EnvioNewletterRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            EnvioNewletterDto model = _unitOfWork.EnvioNewletterRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.EnvioNewletterRepository.Edit(model);
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

        ~EnvioNewletter()
        {
            Clear(false);
        }
    }
}

