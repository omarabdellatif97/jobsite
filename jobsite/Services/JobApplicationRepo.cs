using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }













}
