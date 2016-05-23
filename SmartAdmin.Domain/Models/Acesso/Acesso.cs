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
    public abstract class Acesso
    {
        public SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public virtual void Save(AcessoDto model)
        {
            _unitOfWork.AcessoRepository.Add(model);
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public virtual AcessoDto SaveGetItem(AcessoDto model)
        {
           var retorno = _unitOfWork.AcessoRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public virtual void SaveAll(List<AcessoDto> model)
        {
            _unitOfWork.AcessoRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public virtual void Edit(AcessoDto model)
        {
            _unitOfWork.AcessoRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public virtual AcessoDto GetItem(Expression<Func<AcessoDto, bool>> filter)
        {
            AcessoDto model;
            model = _unitOfWork.AcessoRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        public virtual void Delete(Expression<Func<AcessoDto, bool>> filter)
        {
            var Collection = _unitOfWork.AcessoRepository.GetByFilter(filter);
            if (Collection.ToList().Count > 0)
            {
                foreach (var item in Collection)
                {
                    _unitOfWork.AcessoRepository.Delete(item);
                }
            }
        }

        /// <summary>
        /// Deleta uma lista de objetos
        /// </summary>
        public virtual void DeleteAll(List<AcessoDto> collection)
        {
            foreach (var item in collection) { _unitOfWork.AcessoRepository.Delete(item); }
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual List<AcessoDto> GetList(Expression<Func<AcessoDto, bool>> filter)
        {
            List<AcessoDto> collection;
            collection = _unitOfWork.AcessoRepository.GetByFilter(filter).ToList();
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista IQueryable(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public virtual IQueryable<AcessoDto> GetByFilter(Expression<Func<AcessoDto, bool>> filter)
        {
            var collection = _unitOfWork.AcessoRepository.GetByFilter(filter);
            return (collection);
        }

    }
}

