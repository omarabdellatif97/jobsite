using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
    [Table("JobPost")]
    public class JobPost : EntityBase
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Title { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "max length is 300 character")]
        public string Description { get; set; }


        [Required]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Location { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }

        [Required]
        public JobPostStatus Status { get; set; }


        [MaxLength(400)]
        public string KeywordsText
        {
            get;
            set;
        }

        [NotMapped]
        public List<string> KeywordsList
        {
            get => KeywordsText.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }

        //public virtual ICollection<Keyword> Keywords { get; set; } = new HashSet<Keyword>();

        public virtual ICollection<JobApplication> Applications { get; set; } = new HashSet<JobApplication>();



    }





}
