using jobsite.Models;

namespace jobsite.Services
{
    public interface ICandidateRepo : IRepository<Candidate>
    {
        IEnumable<CV> GetCVs(int id);
        IEnumable<CV> GetJobApplications(int id);
    }













}
