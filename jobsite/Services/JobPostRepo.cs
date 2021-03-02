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

            base.Update(entity);
        }
    }













}
