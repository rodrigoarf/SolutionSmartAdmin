using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SmartAdmin.Data;
using SmartAdmin.Data.Model;

/// <summary>
/// Esta classe abstrata não pode ser instanciada. O objetivo desta classe é fornecer uma definição de metodos
/// base comuns para que várias outras classes derivadas desta possam compartilhar metodos por 'override'.
/// </summary>
namespace SmartAdmin.Domain.Model
{
    public abstract class MenuUsuario
    {
        public SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public virtual void Save(MenuUsuarioDto model)
        {
            _unitOfWork.MenuUsuarioRepository.Add(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public virtual MenuUsuarioDto SaveGetItem(MenuUsuarioDto model)
        {
           var retorno = _unitOfWork.MenuUsuarioRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public virtual void SaveAll(List<MenuUsuarioDto> model)
        {
            _unitOfWork.MenuUsuarioRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public virtual void Edit(MenuUsuarioDto model)
        {
            _unitOfWork.MenuUsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public virtual MenuUsuarioDto GetItem(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            MenuUsuarioDto model;
            model = _unitOfWork.MenuUsuarioRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public virtual void Delete(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            var Collection = _unitOfWork.MenuUsuarioRepository.GetByFilter(filter);
            if (Collection.ToList().Count > 0)
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
        public virtual void DeleteAll(List<MenuUsuarioDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.MenuUsuarioRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual List<MenuUsuarioDto> GetList(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            List<MenuUsuarioDto> collection;
            collection = _unitOfWork.MenuUsuarioRepository.GetByFilter(filter).ToList();
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual IQueryable<MenuUsuarioDto> GetByFilter(Expression<Func<MenuUsuarioDto, bool>> filter)
        {
            var collection = _unitOfWork.MenuUsuarioRepository.GetByFilter(filter);
            return (collection);
        }

    }
}

