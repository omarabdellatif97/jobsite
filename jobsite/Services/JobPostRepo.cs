using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class JobPostRepo : Repository<JobPost>,IJobPostRepo
    {
        public JobPostRepo(JobContext context) : base(context)
        {
        }

        public override JobPost Get(int id)
        {
            //return base.Get(id);

            return context.JobPosts.Include(j => j.Keywords)
                .Include(j=> j.Department)
                .SingleOrDefault(j => j.Id == id);
        }

        public override IEnumerable<JobPost> GetAll()
        {
            //return base.GetAll();
            return context.JobPosts.Include(j => j.Keywords)
                .Include(j => j.Department).ToList();
        }
    }













}
