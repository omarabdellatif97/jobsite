using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jobsite.Models;
using Microsoft.Extensions.Configuration;

namespace jobsite.Models
{
    public class JobContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public IConfiguration Configuration { get; }
        public JobContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator(u => u.Discriminator);

            modelBuilder.Entity<JobApplication>()
                .Property(a => a.AppDate)
                .HasDefaultValue(DateTime.Now);


            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new Admin
            {
                Id = -1,
                UserName = Configuration["DefaultUserNameInfo:Email"],
                Email = Configuration["DefaultUserNameInfo:Email"],
                NormalizedEmail = Configuration["DefaultUserNameInfo:Email"].ToUpper(),
                NormalizedUserName = Configuration["DefaultUserNameInfo:Email"].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("N").ToUpper(),
                Address = "",
                BirthDate = DateTime.Now,
                Gender = Gender.Male,
                Name = "Admin",
                PasswordHash = hasher.HashPassword(null, Configuration["DefaultUserNameInfo:Password"]),
                EmailConfirmed = true
            };

            modelBuilder.Entity<Admin>().HasData(user);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<jobsite.Models.Education> Education { get; set; }

    }


}
