using jobsite.Models;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public interface IJobPostRepo : IRepository<JobPost>
    {
        JobPost GetWithApplications(int id);
        void closeJobPost(JobPost entity);
    }













}
