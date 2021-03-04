using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly JobContext context;
        private bool disposedValue;

        public Repository(JobContext context)
        {
            this.context = context;
        }

        public virtual TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            //return context.Set<TEntity>().ToList();
            return context.Set<TEntity>().AsNoTracking().ToList();
        }

        public virtual IEnumerable<TEntity> GetAllIEnumerable()
        {
            //return context.Set<TEntity>().ToList();
            return context.Set<TEntity>().AsNoTracking();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            //return context.Set<TEntity>().ToList();
            return context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        //public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return context.Set<TEntity>().Where(predicate);
        //}

        //public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return context.Set<TEntity>().SingleOrDefault(predicate);
        //}

        public virtual Task<TEntity> GetAsync(int id)
        {
            return context.Set<TEntity>().FindAsync(id).AsTask();
        }
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllIEnumerable().ToList();
        }
        public virtual IEnumerable<TEntity> GetAllIEnumerable(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }
        public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).ToListAsync();
        }


        public virtual void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        //public virtual IEnumerable<TEntity> Search(string value)
        //{
        //    throw new NotImplementedException();
        //}


        public virtual void Update(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            this.context.SaveChanges();
        }

        public virtual Task<int> SaveAsync()
        {
            return this.context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public abstract Task<List<TEntity>> SearchAsync(string jobsearch);
        public abstract IEnumerable<TEntity> Search(string jobsearch);
    }
}
