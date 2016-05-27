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
    public abstract class NoticiaCategoria
    {
        public SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public virtual void Save(NoticiaCategoriaDto model)
        {
            _unitOfWork.NoticiaCategoriaRepository.Add(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public virtual NoticiaCategoriaDto SaveGetItem(NoticiaCategoriaDto model)
        {
           var retorno = _unitOfWork.NoticiaCategoriaRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public virtual void SaveAll(List<NoticiaCategoriaDto> model)
        {
            _unitOfWork.NoticiaCategoriaRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public virtual void Edit(NoticiaCategoriaDto model)
        {
            _unitOfWork.NoticiaCategoriaRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public virtual NoticiaCategoriaDto GetItem(Expression<Func<NoticiaCategoriaDto, bool>> filter)
        {
            NoticiaCategoriaDto model;
            model = _unitOfWork.NoticiaCategoriaRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public virtual void Delete(Expression<Func<NoticiaCategoriaDto, bool>> filter)
        {
            var Collection = _unitOfWork.NoticiaCategoriaRepository.GetByFilter(filter);
            if (Collection.ToList().Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.NoticiaCategoriaRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public virtual void DeleteAll(List<NoticiaCategoriaDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.NoticiaCategoriaRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual List<NoticiaCategoriaDto> GetList(Expression<Func<NoticiaCategoriaDto, bool>> filter)
        {
            List<NoticiaCategoriaDto> collection;
            collection = _unitOfWork.NoticiaCategoriaRepository.GetByFilter(filter).ToList();
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual IQueryable<NoticiaCategoriaDto> GetByFilter(Expression<Func<NoticiaCategoriaDto, bool>> filter)
        {
            var collection = _unitOfWork.NoticiaCategoriaRepository.GetByFilter(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public virtual void ToInactive(int Id)
        {
            NoticiaCategoriaDto model = _unitOfWork.NoticiaCategoriaRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.NoticiaCategoriaRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public virtual void ToActive(int Id)
        {
            NoticiaCategoriaDto model = _unitOfWork.NoticiaCategoriaRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.NoticiaCategoriaRepository.Edit(model);
        }

    }
}

