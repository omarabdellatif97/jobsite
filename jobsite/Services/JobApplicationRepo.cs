using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class JobApplicationRepo : IJobApplicationRepo
    {
        private JobContext context;

        public JobApplicationRepo(JobContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var entity = context.JobApplications.Find(id);
            context.JobApplications.Remove(entity);
        }

        public JobApplication Get(int id)
        {
            return context.JobApplications.Find(id);
        }

        public IEnumerable<JobApplication> GetAll()
        {
            return context.JobApplications.ToList();
        }

        public void Insert(JobApplication entity)
        {
            context.JobApplications.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(JobApplication entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }


        public IEnumerable<JobApplication> Search(string value)
        {
            return context.JobApplications.Where(e => e.Candidate.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

        }





        #region dispose pattern
        private bool disposedValue;

        public JobContext Context => throw new NotImplementedException();

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
