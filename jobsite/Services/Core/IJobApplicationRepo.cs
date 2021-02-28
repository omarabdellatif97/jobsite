using jobsite.Models;
using System.Collections.Generic;

namespace jobsite.Services
{
    public interface IJobApplicationRepo : IRepository<JobApplication>
    {
        IEnumerable<JobApplication> GetAll(Candidate candidate);
        IEnumerable<JobApplication> GetAll (JobPost post);
        JobApplication Get(Candidate candidate , JobPost post);
    }













}
