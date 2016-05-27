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
    public abstract class NoticiaPublicador
    {
        public SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public virtual void Save(NoticiaPublicadorDto model)
        {
            _unitOfWork.NoticiaPublicadorRepository.Add(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public virtual NoticiaPublicadorDto SaveGetItem(NoticiaPublicadorDto model)
        {
           var retorno = _unitOfWork.NoticiaPublicadorRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public virtual void SaveAll(List<NoticiaPublicadorDto> model)
        {
            _unitOfWork.NoticiaPublicadorRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public virtual void Edit(NoticiaPublicadorDto model)
        {
            _unitOfWork.NoticiaPublicadorRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public virtual NoticiaPublicadorDto GetItem(Expression<Func<NoticiaPublicadorDto, bool>> filter)
        {
            NoticiaPublicadorDto model;
            model = _unitOfWork.NoticiaPublicadorRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public virtual void Delete(Expression<Func<NoticiaPublicadorDto, bool>> filter)
        {
            var Collection = _unitOfWork.NoticiaPublicadorRepository.GetByFilter(filter);
            if (Collection.ToList().Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.NoticiaPublicadorRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public virtual void DeleteAll(List<NoticiaPublicadorDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.NoticiaPublicadorRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual List<NoticiaPublicadorDto> GetList(Expression<Func<NoticiaPublicadorDto, bool>> filter)
        {
            List<NoticiaPublicadorDto> collection;
            collection = _unitOfWork.NoticiaPublicadorRepository.GetByFilter(filter).ToList();
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual IQueryable<NoticiaPublicadorDto> GetByFilter(Expression<Func<NoticiaPublicadorDto, bool>> filter)
        {
            var collection = _unitOfWork.NoticiaPublicadorRepository.GetByFilter(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public virtual void ToInactive(int Id)
        {
            NoticiaPublicadorDto model = _unitOfWork.NoticiaPublicadorRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.NoticiaPublicadorRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public virtual void ToActive(int Id)
        {
            NoticiaPublicadorDto model = _unitOfWork.NoticiaPublicadorRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.NoticiaPublicadorRepository.Edit(model);
        }

    }
}

