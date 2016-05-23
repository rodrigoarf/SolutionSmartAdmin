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
    public abstract class Usuario
    {
        public SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public virtual void Save(UsuarioDto model)
        {
            _unitOfWork.UsuarioRepository.Add(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public virtual UsuarioDto SaveGetItem(UsuarioDto model)
        {
           var retorno = _unitOfWork.UsuarioRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public virtual void SaveAll(List<UsuarioDto> model)
        {
            _unitOfWork.UsuarioRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public virtual void Edit(UsuarioDto model)
        {
            _unitOfWork.UsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public virtual UsuarioDto GetItem(Expression<Func<UsuarioDto, bool>> filter)
        {
            UsuarioDto model;
            model = _unitOfWork.UsuarioRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public virtual void Delete(Expression<Func<UsuarioDto, bool>> filter)
        {
            var Collection = _unitOfWork.UsuarioRepository.GetByFilter(filter);
            if (Collection.ToList().Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.UsuarioRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public virtual void DeleteAll(List<UsuarioDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.UsuarioRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual List<UsuarioDto> GetList(Expression<Func<UsuarioDto, bool>> filter)
        {
            List<UsuarioDto> collection;
            collection = _unitOfWork.UsuarioRepository.GetByFilter(filter).ToList();
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual IQueryable<UsuarioDto> GetByFilter(Expression<Func<UsuarioDto, bool>> filter)
        {
            var collection = _unitOfWork.UsuarioRepository.GetByFilter(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public virtual void ToInactive(int Id)
        {
            UsuarioDto model = _unitOfWork.UsuarioRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.UsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public virtual void ToActive(int Id)
        {
            UsuarioDto model = _unitOfWork.UsuarioRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.UsuarioRepository.Edit(model);
        }

    }
}

