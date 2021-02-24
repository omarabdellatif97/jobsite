using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
    [Table("JobApplication")]
    public class JobApplication : EntityBase
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime AppDate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime Probation{ get; set; }

        [Required]
        public AppStatus AppStatus { get; set; }


        public int JobPostId { get; set; }

        public int CandidateId { get; set; }

        public int CVId { get; set; }

        [ForeignKey("CVId")]
        public virtual CV CV { get; set; }

        [ForeignKey("JobPostId")]
        public virtual JobPost JobPost { get; set; }

        [ForeignKey("CandidateId")]
        public virtual Candidate Candidate { get; set; }

    }





}
