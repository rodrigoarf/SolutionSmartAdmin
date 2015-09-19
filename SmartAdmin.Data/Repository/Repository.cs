using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartAdmin.Data.Context;

namespace SmartAdmin.Data.Generic
{
    public class Repository<TEntity> : IDisposable where TEntity : class
    {
        protected SmartAdminContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(SmartAdminContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Método genérico que salva uma entidade
        /// </summary>
        public void Save(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());

                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Método genérico que atualiza uma entidade
        /// </summary>
        public void Edit(TEntity entity)
        {
            var entry = _context.Entry<TEntity>(entity);
            var pkey = _dbSet.Create().GetType().GetProperty("ID").GetValue(entity);

            if (entry.State ==EntityState.Detached)
            {
                var set = _context.Set<TEntity>();
                TEntity attachedEntity = set.Find(pkey);

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Método genérico que deleta uma entidade, isto é exclui definitivamente da tabela
        /// </summary>
        public void Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Método genérico que salva uma entidade e retorna a mesma salva
        /// </summary>
        public TEntity SaveGetItem(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();

                return (entity);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());

                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Método genérico que salva uma lista de entidades
        /// </summary>
        public void SaveAll(List<TEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    _dbSet.Add(item);
                }
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Método genérico que deleta uma lsita de entidades, isto é exclui definitivamente da tabela
        /// </summary>
        public void DeleteAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            List<TEntity> listDelete = query.Where(filter).ToList();

            foreach (var item in listDelete)
                _dbSet.Remove(item);

            _context.SaveChanges();
        }

        /// <summary>
        /// Método genérico que retorna uma entidade, mediante uma expressão lambda
        /// </summary>
        public TEntity GetItem(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                    query = query.Where(filter);

                return query.ToList().FirstOrDefault();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);
            }
        }

        /// <summary>
        /// Método genérico que retorna uma lista de entidades, mediante uma expressão lambda
        /// </summary>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    return orderBy(query).ToList();
                else
                    return query.ToList();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} Falha da validação ", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException("Erros da validação da Entidade: " + sb.ToString(), ex);

            }
        }

        /// <summary>
        /// Método genérico que exclui o objeto da memória Garbage Collector
        /// </summary>
        public void Dispose()
        {
            _dbSet = null;
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

