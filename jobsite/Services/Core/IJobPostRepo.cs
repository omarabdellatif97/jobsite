using jobsite.Models;

namespace jobsite.Services
{
    public interface IJobPostRepo : IRepository<JobPost>
    {
        JobPost GetWithApplications(int id);
        void closeJobPost(JobPost entity);


    }













}
