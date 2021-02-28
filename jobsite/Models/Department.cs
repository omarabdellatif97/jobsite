using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
    [Table("Department")]
    public class Department : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "max length is 150 character")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "max length is 200 character")]
        public string Description { get; set; }


        public virtual ICollection<JobPost> JobPosts { get; set; } = new HashSet<JobPost>();

    }





}
