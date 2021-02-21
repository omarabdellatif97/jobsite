using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Models
{
    public class JobContext: DbContext
    {
        public JobContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Candidate> Candidates { get; set; }
    }
}
