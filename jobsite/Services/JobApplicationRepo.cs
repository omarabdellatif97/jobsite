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

    }













}
