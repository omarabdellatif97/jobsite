using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public interface IUnitOfWork : IDisposable
    {

        public ICandidateRepo Candidates { get; }
        public ICVRepo CV { get; }
        public IJobPostRepo JobPosts { get; }
        public IJobApplicationRepo JobApplications { get; }
        public IDepartmentRepo Departments { get; }
        public IAdminRepo Admins { get; }
        void Save();
        Task<int> SaveAsync();


    }

}
