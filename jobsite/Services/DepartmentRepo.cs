using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class DepartmentRepo : Repository<Department>, IDepartmentRepo
    {


        public DepartmentRepo(JobContext context) : base(context)
        {
        }

        public override Department Get(int id)
        {
            return base.Get(id);
        }

        public override IEnumerable<Department> GetAll()
        {
            return base.GetAll();
        }

        public override IEnumerable<Department> GetAllIEnumerable()
        {
            return base.GetAllIEnumerable();
        }

        public override Task<List<Department>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<List<Department>> SearchAsync(string jobsearch)
        {
            return GetAllAsync(j => j.Name.Contains(jobsearch));
        }

        public override IEnumerable<Department> Search(string jobsearch)
        {
            return GetAll(j => j.Name.Contains(jobsearch));
        }
    }













}
