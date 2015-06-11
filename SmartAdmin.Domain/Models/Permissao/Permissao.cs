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
    public partial class Permissao
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto
        /// </summary>
        public void Save(PermissaoDto model)
        {
            _unitOfWork.PermissaoRepository.AddItem(model);
        }

        /// <summary>
        /// Salva e retorna um objeto
        /// </summary>
        public PermissaoDto SaveGetItem(PermissaoDto model)
        {
           var retorno = _unitOfWork.PermissaoRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos
        /// </summary>
        public void SaveAll(List<PermissaoDto> model)
        {
            _unitOfWork.PermissaoRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto
        /// </summary>
        public void Edit(PermissaoDto model)
        {
            _unitOfWork.PermissaoRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto buscado por expressão Lambda
        /// </summary>
        public PermissaoDto GetItem(Expression<Func<PermissaoDto, bool>> filter)
        {
            PermissaoDto model;
            model = _unitOfWork.PermissaoRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna um objeto do tipo List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<PermissaoDto> GetList(Expression<Func<PermissaoDto, bool>> filter)
        {
            List<PermissaoDto> collection;
            collection = _unitOfWork.PermissaoRepository.GetList(filter);
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

        ~Permissao()
        {
            Clear(false);
        }
    }
}

