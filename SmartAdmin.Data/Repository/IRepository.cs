using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Repository
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity Model);
        void Edit(TEntity Model);
        void Delete(TEntity Model);
        void DeleteAll(Expression<Func<TEntity, bool>> Filter = null);
        TEntity AddGetItem(TEntity Model);
        TEntity UpdateGetItem(TEntity Model);
        TEntity GetItem(Expression<Func<TEntity, bool>> Filter = null);
        IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null);
    }
}

