using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private JobContext context;

        public DepartmentRepo(JobContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var entity = context.Departments.Find(id);
            context.Departments.Remove(entity);
        }

        public Department Get(int id)
        {
            return context.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public void Insert(Department entity)
        {
            context.Departments.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public IEnumerable<Department> Search(string value)
        {
            return context.Departments.Where(e => e.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

        }

        public void Update(Department entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        #region dispose pattern
        private bool disposedValue;

        //public JobContext Context => throw new NotImplementedException();

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
        // ~JobPostRepo()
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
        #endregion


        

    }













}
