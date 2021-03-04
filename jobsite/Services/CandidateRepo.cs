using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public override IEnumerable<Candidate> GetAllIEnumerable()
        {
            return context.Candidates.Include(c => c.Educations);
        }

        public override Task<List<Candidate>> GetAllAsync()
        {
            return context.Candidates.Include(c => c.Educations).ToListAsync();
        }



        public override Candidate Get(Expression<Func<Candidate, bool>> predicate)
        {
            //return base.Get(predicate);
            return context.Candidates
                .Include(j => j.Educations)
                .FirstOrDefault(predicate);
        }

        public override Task<Candidate> GetAsync(Expression<Func<Candidate, bool>> predicate)
        {
            //return base.GetAsync(predicate);
            return context.Candidates
                    .Include(j => j.Educations)
                    .FirstOrDefaultAsync(predicate);
        }

        public override Task<Candidate> GetAsync(int id)
        {
            //return base.GetAsync(id);
            return context.Candidates.Include(j => j.Educations ).FirstOrDefaultAsync(j => j.Id == id);
        }

        public override IEnumerable<Candidate> GetAll(Expression<Func<Candidate, bool>> predicate)
        {
            //return base.GetAll(predicate);
            return GetAllIEnumerable(predicate).ToList();
        }

        public override IEnumerable<Candidate> GetAllIEnumerable(Expression<Func<Candidate, bool>> predicate)
        {
            //return base.GetAllIEnumerable(predicate);
            return context.Candidates.Include(j => j.Educations).Where(predicate);
        }

        public override Task<List<Candidate>> GetAllAsync(Expression<Func<Candidate, bool>> predicate)
        {
            //return base.GetAllAsync(predicate);
            return context.Candidates.Include(j => j.Educations ).Where(predicate).ToListAsync();
        }

    }













}
