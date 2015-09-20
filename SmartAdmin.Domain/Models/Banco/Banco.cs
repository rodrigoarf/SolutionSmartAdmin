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
    public partial class Banco
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(BancoDto model)
        {
            model.SIGLA = model.SIGLA.ToUpper();
            _unitOfWork.BancoRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public BancoDto SaveGetItem(BancoDto model)
        {
           var retorno = _unitOfWork.BancoRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<BancoDto> model)
        {
            _unitOfWork.BancoRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(BancoDto model)
        {
            _unitOfWork.BancoRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public BancoDto GetItem(Expression<Func<BancoDto, bool>> filter)
        {
            BancoDto model;
            model = _unitOfWork.BancoRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public void Delete(Expression<Func<BancoDto, bool>> filter)
        {
            var model = _unitOfWork.BancoRepository.GetItem(filter);
            _unitOfWork.BancoRepository.Delete(model);
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public void DeleteAll(List<BancoDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.BancoRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<BancoDto> GetList(Expression<Func<BancoDto, bool>> filter)
        {
            List<BancoDto> collection;
            collection = _unitOfWork.BancoRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            BancoDto model = _unitOfWork.BancoRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.BancoRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            BancoDto model = _unitOfWork.BancoRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.BancoRepository.Edit(model);
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

        ~Banco()
        {
            Clear(false);
        }
    }
}

