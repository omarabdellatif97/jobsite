using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class CVRepo : Repository<CV>, ICVRepo
    {

        public CVRepo(JobContext context) : base(context)
        {

        }

        public override CV Get(int id)
        {
            return base.Get(id);
            //return context.CVs.Include(cv=>job)
        }

        public override IEnumerable<CV> GetAll()
        {
            return base.GetAll();
        }


        public override IEnumerable<CV> GetAllIEnumerable()
        {
            return base.GetAllIEnumerable();
        }

        public override Task<List<CV>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<List<CV>> SearchAsync(string jobsearch)
        {
            return GetAllAsync(j => j.Title.Contains(jobsearch)
                 );
        }

        public override IEnumerable<CV> Search(string jobsearch)
        {
            return GetAll(j => j.Title.Contains(jobsearch)
                );
        }
    }



}