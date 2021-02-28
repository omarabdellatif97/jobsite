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



        //TEntity Get(int id);
        //IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        //void Add(TEntity entity);
        //void AddRange(IEnumerable<TEntity> entities);
        //void Update(TEntity entity);
        //void Remove(TEntity entity);
        //void RemoveRange(IEnumerable<TEntity> entities);
        //void Save();
        //Task<int> SaveAsync();


        public override Candidate Get(int id)
        {
            return context.Candidates.Include(c=> c.Educations).FirstOrDefault(c=> c.Id == id);
        }

        public override IEnumerable<Candidate> GetAll()
        {
            return context.Candidates.Include(c => c.Educations).ToList();
        }

        public IEnumerable<Candidate> GetAll(JobPost post)
        {
            return context.JobPosts.Where(j => j.Id == post.Id).SelectMany(j => j.Applications.Select(a => a.Candidate)).ToList();
        }
    }













}
