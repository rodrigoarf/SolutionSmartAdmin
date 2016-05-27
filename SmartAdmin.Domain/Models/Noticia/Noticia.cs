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
    public abstract class Noticia
    {
        public SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public virtual void Save(NoticiaDto model)
        {
            _unitOfWork.NoticiaRepository.Add(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public virtual NoticiaDto SaveGetItem(NoticiaDto model)
        {
           var retorno = _unitOfWork.NoticiaRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public virtual void SaveAll(List<NoticiaDto> model)
        {
            _unitOfWork.NoticiaRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public virtual void Edit(NoticiaDto model)
        {
            _unitOfWork.NoticiaRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public virtual NoticiaDto GetItem(Expression<Func<NoticiaDto, bool>> filter)
        {
            NoticiaDto model;
            model = _unitOfWork.NoticiaRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public virtual void Delete(Expression<Func<NoticiaDto, bool>> filter)
        {
            var Collection = _unitOfWork.NoticiaRepository.GetByFilter(filter);
            if (Collection.ToList().Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.NoticiaRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public virtual void DeleteAll(List<NoticiaDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.NoticiaRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual List<NoticiaDto> GetList(Expression<Func<NoticiaDto, bool>> filter)
        {
            List<NoticiaDto> collection;
            collection = _unitOfWork.NoticiaRepository.GetByFilter(filter).ToList();
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual IQueryable<NoticiaDto> GetByFilter(Expression<Func<NoticiaDto, bool>> filter)
        {
            var collection = _unitOfWork.NoticiaRepository.GetByFilter(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public virtual void ToInactive(int Id)
        {
            NoticiaDto model = _unitOfWork.NoticiaRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.NoticiaRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public virtual void ToActive(int Id)
        {
            NoticiaDto model = _unitOfWork.NoticiaRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.NoticiaRepository.Edit(model);
        }

    }
}

