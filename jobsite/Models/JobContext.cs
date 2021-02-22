using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Models
{
    public class JobContext: DbContext
    {
        public JobContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Admin> Admins{ get; set; }
        public DbSet<Keyword> Keywords{ get; set; }
        public DbSet<Department> Departments { get; set; }



    }


}
