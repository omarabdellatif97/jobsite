using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jobsite.Models;

namespace jobsite.Models
{
    public class JobContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public JobContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
                
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<CV> CVs { get; set; }
        //public DbSet<Keyword> Keywords{ get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator(u => u.Discriminator);

            modelBuilder.Entity<JobApplication>()
                .Property(a => a.AppDate)
                .HasDefaultValueSql("getdate()");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<jobsite.Models.Education> Education { get; set; }

    }


}
