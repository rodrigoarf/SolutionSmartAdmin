using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Configuration;
using SmartAdmin.Data.Repository;
using SmartAdmin.Data.ApplicationContext;

namespace SmartAdmin.Data.Repository
{
    public class RepositoryGeneric<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _DbSet;
        private SmartAdminContext _Context;

        public RepositoryGeneric(SmartAdminContext Context)
        {
            this._Context = Context;
            this._DbSet = Context.Set<TEntity>();
        }

        public void Add(TEntity Model)
        {
            try
            {
                _DbSet.Add(Model);
                _Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public TEntity AddGetItem(TEntity Model)
        {
            try
            {
                _DbSet.Add(Model);
                _Context.SaveChanges();
                return (Model);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public void AddAll(List<TEntity> Collection)
        {
            foreach (var model in Collection) { this.Add(model); }
        }

        public void Edit(TEntity Model)
        {
            //_DbSet.Attach(Model);
            //_Context.SaveChanges();
            // Or -->...
            try
            {
                var entry = _Context.Entry<TEntity>(Model);
                var pkey = _DbSet.Create().GetType().GetProperty("ID").GetValue(Model);

                if (entry.State == System.Data.EntityState.Detached)
                {
                    var set = _Context.Set<TEntity>();
                    TEntity AttachedEntity = set.Find(pkey);

                    if (AttachedEntity != null)
                    {
                        var AttachedEntry = _Context.Entry(AttachedEntity);
                        AttachedEntry.CurrentValues.SetValues(Model);
                    }
                    else
                    {
                        entry.State = System.Data.EntityState.Modified;
                    }
                }

                if (entry.State == System.Data.EntityState.Detached)
                {
                    var set = _Context.Set<TEntity>();
                    TEntity AttachedEntity = set.Find(pkey);

                    if (AttachedEntity != null)
                    {
                        var AttachedEntry = _Context.Entry(AttachedEntity);
                        AttachedEntry.CurrentValues.SetValues(Model);
                    }
                    else
                    {
                        entry.State = System.Data.EntityState.Modified;
                    }
                }

                _Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public TEntity UpdateGetItem(TEntity Model)
        {
            //_DbSet.Attach(Model);
            //_Context.SaveChanges();
            // Or -->...
            try
            {
                var entry = _Context.Entry<TEntity>(Model);
                var pkey = _DbSet.Create().GetType().GetProperty("ID").GetValue(Model);

                if (entry.State == System.Data.EntityState.Detached)
                {
                    var set = _Context.Set<TEntity>();
                    TEntity AttachedEntity = set.Find(pkey);

                    if (AttachedEntity != null)
                    {
                        var AttachedEntry = _Context.Entry(AttachedEntity);
                        AttachedEntry.CurrentValues.SetValues(Model);
                    }
                    else
                    {
                        entry.State = System.Data.EntityState.Modified;
                    }
                }

                _Context.SaveChanges();
                return (Model);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public void Delete(TEntity Model)
        {
            try
            {
                _DbSet.Remove(Model);
                _Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public TEntity GetItem(Expression<Func<TEntity, bool>> Filter = null)
        {
            try
            {
                IQueryable<TEntity> Query = _DbSet;
                if (Filter != null) { Query = Query.Where(Filter); }
                return Query.ToList().FirstOrDefault();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> Filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy = null)
        {
            try
            {
                IQueryable<TEntity> Query = _DbSet;
                if (Filter != null) { Query = Query.Where(Filter); }
                if (OrderBy != null) { return OrderBy(Query); } else { return Query; }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
                throw ExceptionValidationError(Ex);
            }
        }

        public void DeleteAll(Expression<Func<TEntity, bool>> Filter = null)
        {
            try
            {
                IQueryable<TEntity> Query = _DbSet;
                List<TEntity> CollectionTEntity = Query.Where(Filter).ToList();

                foreach (var ItemTEntity in CollectionTEntity)
                {
                    _DbSet.Remove(ItemTEntity);
                }

                _Context.SaveChanges();
            }
            catch (Exception Ex)
            {
                CreateFileLog(Ex.InnerException.Message);
            }
        }

        private Exception ExceptionValidationError(System.Data.Entity.Validation.DbEntityValidationException Ex)
        {
            Exception Raise = Ex;
            foreach (var ValidationErrors in Ex.EntityValidationErrors)
            {
                foreach (var ValidationError in ValidationErrors.ValidationErrors)
                {
                    var Message = String.Format("{0}:{1}", ValidationErrors.Entry.Entity.ToString(), ValidationError.ErrorMessage);
                    Raise = new InvalidOperationException(Message, Raise);
                }
            }
            return (Raise);
        }

        private void CreateFileLog(String ErrorMessage, String PathLog = "")
        {
            var Path = (String.IsNullOrEmpty(PathLog) ? String.Format(ConfigurationManager.AppSettings["Log"].ToString()) : PathLog);

            if (!System.IO.File.Exists(Path))
            {
                using (System.IO.StreamWriter SWriter = System.IO.File.CreateText(Path))
                {
                    SWriter.WriteLine(ErrorMessage);
                }
            }
        }

        public void Dispose()
        {
            _DbSet = null;
            _Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

