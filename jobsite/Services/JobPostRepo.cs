using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class JobPostRepo : Repository<JobPost>,IJobPostRepo
    {
        public JobPostRepo(JobContext context) : base(context)
        {
        }

        public override void Add(JobPost entity)
        {
            base.Add(entity);

        }

        public override void AddRange(IEnumerable<JobPost> entities)
        {
            base.AddRange(entities);
        }

        public override JobPost Get(int id)
        {
            //return base.Get(id);

            return context.JobPosts
                .Include(j=> j.Department)
                .SingleOrDefault(j => j.Id == id);
        }


        public JobPost GetWithApplications(int id)
        {
            //return base.Get(id);

            return context.JobPosts
                .Include(j => j.Department)
                .Include(j=> j.Applications)
                .SingleOrDefault(j => j.Id == id);
        }

        public override IEnumerable<JobPost> GetAll()
        {
            //return base.GetAll();
            return context.JobPosts
                .Include(j => j.Department).ToList();
        }

        public override IEnumerable<JobPost> GetAllIEnumerable()
        {
            return context.JobPosts
                .Include(j => j.Department);
        }

        public override Task<List<JobPost>> GetAllAsync()
        {
            return context.JobPosts
                .Include(j => j.Department).ToListAsync();
        }

        

        public override void Remove(JobPost entity)
        {
            //this.context.Keywords.RemoveRange(this.context.Keywords.Where(k => k.JobPostId==entity.Id ));
            base.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<JobPost> entities)
        {
            //this.context.Keywords.RemoveRange(this.context.Keywords.Where(k => entities.Contains(k.JobPost)));
            base.RemoveRange(entities);
        }

        public override void Update(JobPost entity)
        {
            var post = Get(entity.Id);
            post.KeywordsText = entity.KeywordsText;
            post.Location = entity.Location;
            post.Title = entity.Title;
            post.Status = entity.Status;
            post.Description = entity.Description;
            post.DeptId = entity.DeptId;

            context.JobPosts.Update(post);
        }

        public void closeJobPost(JobPost entity)
        {
            var post = Get(entity.Id);
            post.Status = JobPostStatus.Closed;
            context.JobPosts.Update(post);
        }

        public override JobPost Get(Expression<Func<JobPost, bool>> predicate)
        {
            //return base.Get(predicate);
            return context.JobPosts
                .Include(j => j.Department)
                .FirstOrDefault(predicate);
        }

        public override Task<JobPost> GetAsync(Expression<Func<JobPost, bool>> predicate)
        {
            //return base.GetAsync(predicate);
            return context.JobPosts
                    .Include(j => j.Department)
                    .FirstOrDefaultAsync(predicate);
        }

        public override Task<JobPost> GetAsync(int id)
        {
            //return base.GetAsync(id);
            return context.JobPosts.Include(j => j.Department).FirstOrDefaultAsync(j => j.Id == id);
        }

        public override IEnumerable<JobPost> GetAll(Expression<Func<JobPost, bool>> predicate)
        {
            //return base.GetAll(predicate);
            return GetAllIEnumerable(predicate).ToList();
        }

        public override IEnumerable<JobPost> GetAllIEnumerable(Expression<Func<JobPost, bool>> predicate)
        {
            //return base.GetAllIEnumerable(predicate);
            return context.JobPosts.Include(j => j.Department).Where(predicate);
        }

        public override Task<List<JobPost>> GetAllAsync(Expression<Func<JobPost, bool>> predicate)
        {
            //return base.GetAllAsync(predicate);
            return context.JobPosts.Include(j => j.Department).Where(predicate).ToListAsync();
        }

        public override Task<List<JobPost>> SearchAsync(string jobsearch)
        {
            return GetAllAsync(j => j.Title.Contains(jobsearch)
                || j.Description.Contains(jobsearch)
                || j.Location.Contains(jobsearch)
                || j.KeywordsText.Contains(jobsearch)
                );
        }

        public override IEnumerable<JobPost> Search(string jobsearch)
        {
            return GetAll(j => j.Title.Contains(jobsearch)
                || j.Description.Contains(jobsearch)
                || j.Location.Contains(jobsearch)
                || j.KeywordsText.Contains(jobsearch)
                );
        }
    }













}
