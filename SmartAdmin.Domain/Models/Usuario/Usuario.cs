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
    public partial class Usuario
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto
        /// </summary>
        public void Save(UsuarioDto model)
        {
            _unitOfWork.UsuarioRepository.AddItem(model);
        }

        /// <summary>
        /// Salva e retorna um objeto
        /// </summary>
        public UsuarioDto SaveGetItem(UsuarioDto model)
        {
           var retorno = _unitOfWork.UsuarioRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos
        /// </summary>
        public void SaveAll(List<UsuarioDto> model)
        {
            _unitOfWork.UsuarioRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto
        /// </summary>
        public void Edit(UsuarioDto model)
        {
            _unitOfWork.UsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto buscado por expressão Lambda
        /// </summary>
        public UsuarioDto GetItem(Expression<Func<UsuarioDto, bool>> filter)
        {
            UsuarioDto model;
            model = _unitOfWork.UsuarioRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna um objeto do tipo List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<UsuarioDto> GetList(Expression<Func<UsuarioDto, bool>> filter)
        {
            List<UsuarioDto> collection;
            collection = _unitOfWork.UsuarioRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            UsuarioDto model = _unitOfWork.UsuarioRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.UsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Anativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            UsuarioDto model = _unitOfWork.UsuarioRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.UsuarioRepository.Edit(model);
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

        ~Usuario()
        {
            Clear(false);
        }
    }
}

