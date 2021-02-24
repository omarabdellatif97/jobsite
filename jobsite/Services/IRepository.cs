using jobsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public interface IRepository<T> : IDisposable
        where T : IEntity
    {
        //public JobContext Context { get; }
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string value);
        T Get(int id);
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);

        void Save();
        Task<int> SaveAsync();
    }













}
