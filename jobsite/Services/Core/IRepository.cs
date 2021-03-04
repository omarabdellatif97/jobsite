using jobsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jobsite.Services
{
    //public interface IRepository<TEntity> : IDisposable
    //    where TEntity : IEntity
    //{
    //    //public JobContext Context { get; }
    //    IEnumerable<TEntity> GetAll();
    //    IEnumerable<TEntity> Search(string value);
    //    TEntity Get(int id);
    //    void Insert(TEntity entity);
    //    void Delete(int id);
    //    void Update(TEntity entity);
    //    void Save();
    //    Task<int> SaveAsync();

    //}




    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        //IEnumerable<TEntity> Search(string value);
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllIEnumerable();
        Task<List<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>>predicate);
        IEnumerable<TEntity> GetAllIEnumerable(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Save();
        Task<int> SaveAsync();

    }








}
