using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Models
{

    [Table("Candidate")]
    public class Candidate : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Name { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Required]
        public Gender Gender{ get; set; }

        [Required]
        [MaxLength(150,ErrorMessage ="address can't be more than 150 character")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        [MinLength(8, ErrorMessage = "min length is 8 characters")]
        public string Password { get; set; }


        public virtual ICollection<JobApplication> JobApplications { get; set; } = new HashSet<JobApplication>();
        
        public virtual ICollection<CV> CVs { get; set; } = new HashSet<CV>();



    }





}
