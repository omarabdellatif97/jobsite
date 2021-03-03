using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class JobApplicationRepo : Repository<JobApplication>, IJobApplicationRepo
    {
        public JobApplicationRepo(JobContext context) : base(context)
        {

        }

        public override JobApplication Get(int id)
        {
            return context.JobApplications.Include(j => j.JobPost).Include(j => j.Candidate)
               .SingleOrDefault(j => j.Id == id);
        }

        public JobApplication Get(Candidate candidate, JobPost post)
        {
            return context.JobApplications.Include(j=>j.JobPost).Include(j=> j.Candidate)
                .SingleOrDefault(j => j.CandidateId == candidate.Id && j.JobPostId == post.Id);
        }

        public override IEnumerable<JobApplication> GetAll()
        {
            return context.JobApplications.Include(j => j.JobPost).Include(j => j.Candidate).ToList();
        }

        public IEnumerable<JobApplication> GetAll(Candidate candidate)
        {
            return context.JobApplications.Include(j => j.JobPost).Include(j => j.Candidate).Where(j => j.CandidateId == candidate.Id);
        }

        public IEnumerable<JobApplication> GetAll(JobPost post)
        {
            return context.JobApplications
                .Include(j => j.JobPost).Include(j => j.Candidate)
                .Where(j => j.JobPostId == post.Id);
        }

        public override IEnumerable<JobApplication> GetAllIEnumerable()
        {
            return context.JobApplications
                .Include(j => j.JobPost)
                .Include(j => j.Candidate);
        }

        public override Task<List<JobApplication>> GetAllAsync()
        {
            return context.JobApplications
                .Include(j => j.JobPost)
                .Include(j => j.Candidate).ToListAsync();

        }



        public override JobApplication Get(Expression<Func<JobApplication, bool>> predicate)
        {
            //return base.Get(predicate);
            return context.JobApplications
                .Include(j => j.JobPost)
                .Include(j => j.Candidate)
                .FirstOrDefault(predicate);
        }

        public override Task<JobApplication> GetAsync(Expression<Func<JobApplication, bool>> predicate)
        {
            //return base.GetAsync(predicate);
            return context.JobApplications
                    .Include(j => j.JobPost)
                .Include(j => j.Candidate)
                    .FirstOrDefaultAsync(predicate);
        }

        public override Task<JobApplication> GetAsync(int id)
        {
            //return base.GetAsync(id);
            return context.JobApplications
                .Include(j => j.JobPost)
                .Include(j => j.Candidate)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public override IEnumerable<JobApplication> GetAll(Expression<Func<JobApplication, bool>> predicate)
        {
            //return base.GetAll(predicate);
            return GetAllIEnumerable(predicate).ToList();
        }

        public override IEnumerable<JobApplication> GetAllIEnumerable(Expression<Func<JobApplication, bool>> predicate)
        {
            //return base.GetAllIEnumerable(predicate);
            return context.JobApplications
                .Include(j => j.JobPost)
                .Include(j => j.Candidate)
                .Where(predicate);
        }

        public override Task<List<JobApplication>> GetAllAsync(Expression<Func<JobApplication, bool>> predicate)
        {
            //return base.GetAllAsync(predicate);
            return context.JobApplications
                .Include(j => j.JobPost)
                .Include(j => j.Candidate).Where(predicate).ToListAsync();
        }



    }













}
