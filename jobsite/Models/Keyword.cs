using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
    [Table("Keyword")]
    public class Keyword : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "max length is 150 character")]
        public string Value { get; set; }

        public virtual int JobPostId { get; set; }


        [ForeignKey("JobPostId")]
        public virtual JobPost JobPost { get; set; }


        // i changed keword jobpost relation to one to many not many to many

        //public virtual ICollection<JobPost> JobPosts { get; set; } = new HashSet<JobPost>();
    }





}
