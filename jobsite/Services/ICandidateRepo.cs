using jobsite.Models;
using System.Collections.Generic;

namespace jobsite.Services
{
    public interface ICandidateRepo : IRepository<Candidate>
    {
        IEnumerable<CV> GetJobApplications(int id);
    }













}
