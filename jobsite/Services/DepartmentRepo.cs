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


    }













}
