using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class CandidateRepo : ICandidateRepo
    {
        private JobContext context;

        public CandidateRepo(JobContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var entity = context.Candidates.Find(id);
            context.Candidates.Remove(entity);
        }

        public Candidate Get(int id)
        {
            return context.Candidates.Find(id);

        }

        public IEnumerable<Candidate> GetAll()
        {
            return context.Candidates.ToList();
        }

        public void Insert(Candidate entity)
        {
            context.Candidates.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Candidate entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Candidate> Search(string value)
        {
            return context.Candidates.Where(e => e.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

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

        public IEnumable<CV> GetCVs(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumable<CV> GetJobApplications(int id)
        {
            throw new NotImplementedException();
        }

    }













}
