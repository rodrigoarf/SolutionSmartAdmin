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
    public partial class MenuUsuario
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(MenuUsuarioDto model)
        {
            _unitOfWork.MenuUsuarioRepository.Save(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public MenuUsuarioDto SaveGetItem(MenuUsuarioDto model)
        {
           var retorno = _unitOfWork.MenuUsuarioRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<MenuUsuarioDto> model)
        {
            _unitOfWork.MenuUsuarioRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(MenuUsuarioDto model)
        {
            _unitOfWork.MenuUsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public MenuUsuarioDto GetItem(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            MenuUsuarioDto model;
            model = _unitOfWork.MenuUsuarioRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<MenuUsuarioDto> GetList(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            List<MenuUsuarioDto> collection;
            collection = _unitOfWork.MenuUsuarioRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public void Delete(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            var Collection = _unitOfWork.MenuUsuarioRepository.GetList(filter);
            if (Collection.Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.MenuUsuarioRepository.Delete(item);  
                }
            }            
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public void DeleteAll(List<MenuUsuarioDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.MenuUsuarioRepository.Delete(item); }
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

        ~MenuUsuario()
        {
            Clear(false);
        }
    }
}

