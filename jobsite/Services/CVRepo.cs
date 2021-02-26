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
    }



}