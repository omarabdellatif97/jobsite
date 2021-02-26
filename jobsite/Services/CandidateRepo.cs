using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class CandidateRepo : Repository<Candidate> ,ICandidateRepo
    {

        public CandidateRepo(JobContext context): base(context)
        {
        }

        

        public IEnumerable<CV> GetJobApplications(int id)
        {
            throw new NotImplementedException();
        }
    }













}
