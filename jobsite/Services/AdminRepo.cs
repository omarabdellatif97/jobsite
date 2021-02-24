using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class AdminRepo : IAdminRepo
    {
        private JobContext context;

        public AdminRepo(JobContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var entity = context.Admins.Find(id);
            context.Admins.Remove(entity);
        }

        public Admin Get(int id)
        {
            return context.Admins.Find(id);
        }

        public IEnumerable<Admin> GetAll()
        {
            return context.Admins.ToList();
        }

        public void Insert(Admin entity)
        {
            context.Admins.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Admin entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Admin> Search(string value)
        {
            return context.Admins.Where(e => e.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

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
