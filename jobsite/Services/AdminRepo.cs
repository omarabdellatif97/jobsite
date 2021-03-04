using jobsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Services
{
    public class AdminRepo : Repository<Admin>, IAdminRepo 
    {

        public AdminRepo(JobContext context) : base(context)
        {

        }

        public override Admin Get(int id)
        {
            return base.Get(id);
        }

        public override IEnumerable<Admin> GetAll()
        {
            return base.GetAll();
        }

        public override IEnumerable<Admin> GetAllIEnumerable()
        {
            return base.GetAllIEnumerable();
        }

        public override Task<List<Admin>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
    }













}
