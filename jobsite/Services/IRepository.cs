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
        where TEntity : IEntity
    {
        //IEnumerable<TEntity> Search(string value);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Save();
        Task<int> SaveAsync();

    }








}
