using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
    [Table("Education")]
    public class Education : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        [Required]
        public string School { get; set; }

        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Degree { get; set; }

        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string FieldOfStudy { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Grade { get; set; }

        [MaxLength(255, ErrorMessage = "max length is 255 character")]
        public string Description { get; set; }

        public int CandidateId { get; set; }

        [ForeignKey("CandidateId")]
        virtual public Candidate Candidate { get; set; }



    }



}
