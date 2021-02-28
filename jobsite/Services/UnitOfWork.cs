using jobsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobContext context;

        private ICandidateRepo candidates;
        private IJobPostRepo jobPosts ;
        private IDepartmentRepo departments ;
        private IAdminRepo admins ;
        private ICVRepo cvs ;
        private IJobApplicationRepo jobApplications ;

        public UnitOfWork(JobContext context)
        {
            this.context = context;
        }


        public ICandidateRepo Candidates
        {
            get
            {
                if (candidates == null)
                    candidates = new CandidateRepo(context);
                return candidates;
            }
        }

        public ICVRepo CV
        {
            get
            {
                if (cvs == null)
                    cvs = new CVRepo(context);
                return cvs;
            }
        }

        public IJobPostRepo JobPosts
        {
            get
            {
                
                if (jobPosts == null)
                    jobPosts = new JobPostRepo(context);
                return jobPosts;
            }
        }

        public IJobApplicationRepo JobApplications
        {
            get
            {
                if(jobApplications == null)
                    jobApplications = new JobApplicationRepo(context);
                return jobApplications;
            }
        }

        public IDepartmentRepo Departments
        {
            get
            {
                if(departments == null)
                    departments = new DepartmentRepo(context);
                return departments;
            }
        }

        public IAdminRepo Admins
        {
            get
            {
                if(admins == null)
                    admins = new AdminRepo(context);
                return admins;
            }
        }

        

        public void Save()
        {
            this.context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

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
        // ~UnitOfWork()
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



        //private readonly Lazy<ICandidateRepo> candidates = new Lazy<ICandidateRepo>(()=> new CandidateRepo(context));
        //private readonly Lazy<IJobPostRepo> jobPosts = new Lazy<IJobPostRepo>(()=> new JobPostRepo(context));
        //private readonly Lazy<IDepartmentRepo> departments = new Lazy<IDepartmentRepo>(()=> new DepartmentRepo(context));
        //private readonly Lazy<IAdminRepo> admins = new Lazy<IAdminRepo>(()=> new AdminRepo(context));
        //private readonly Lazy<ICVRepo> cvs = new Lazy<ICVRepo>(()=> new CVRepo(context));

        #endregion

        // next

    }
}
