using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
    [Table("CV")]
    public class CV : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1024*1024*8, ErrorMessage = "max length is 8 migabytes")]
        [DataType(DataType.Upload,ErrorMessage ="Not Valid File Upload")]
        public byte[] Content { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "max length is 20 characters")]
        public string Extension { get; set; }


        public int CandidateId { get; set; }

        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; }


        public virtual ICollection<JobApplication> JobApplications { get; set; } 
            = new HashSet<JobApplication>();

    }





}
