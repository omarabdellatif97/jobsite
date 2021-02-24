using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class JobPostRepo : IJobPostRepo
    {
        private JobContext context;

        public JobPostRepo(JobContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var entity = context.JobPosts.Find(id);
            context.JobPosts.Remove(entity);
        }

        public JobPost Get(int id)
        {
            return context.JobPosts.Find(id);
        }

        public IEnumerable<JobPost> GetAll()
        {
            return context.JobPosts.ToList();

        }

        public void Insert(JobPost entity)
        {
            context.JobPosts.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public IEnumerable<JobPost> Search(string value)
        {
            return context.JobPosts.Where(e => e.Title.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

       
        public void Update(JobPost entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        //public JobContext Context => throw new NotImplementedException();

        #region dispose pattern
        private bool disposedValue;


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
