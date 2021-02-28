using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Models
{
    public class Candidate : ApplicationUser
    {
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new HashSet<JobApplication>(); 
        public virtual ICollection<Education> Educations { get; set; } = new HashSet<Education>();
        //public virtual ICollection<CV> CVs { get; set; } = new HashSet<CV>();
        public int? CVId { get; set; }

        [ForeignKey("CVId")]
        public virtual CV CV { get; set; }
    }
}
