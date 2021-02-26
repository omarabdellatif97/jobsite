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

    }













}
