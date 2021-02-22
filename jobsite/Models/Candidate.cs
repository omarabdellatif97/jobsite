using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Models
{

    public enum Gender
    {
        Male, Female
    }

    public abstract class EntityBase
    {

    }

    [Table("Candidate")]
    public class Candidate : EntityBase
    {
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
        [MaxLength(150,ErrorMessage ="")]
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


    [Table("Admin")]
    public class Admin : EntityBase
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
        public Gender Gender { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "max length is 150 character ")]
        public string Address { get; set; }

    }



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


        public int DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; } = new HashSet<Keyword>();

        public virtual ICollection<JobApplication> Applications { get; set; } = new HashSet<JobApplication>();
    
    
    
    }


    [Table("Keyword")]
    public class Keyword
    {

        public int Id { get; set; }


        public string Value { get; set; }

        public virtual ICollection<JobPost> JobPosts { get; set; } = new HashSet<JobPost>();
    }


    public enum JobPostStatus
    {
        Opened, Closed
    }


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


    public enum AppStatus
    {
        Submited,Viewed,Accepted,Rejected
    }


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

    //[Table("Company")]
    public class Company : EntityBase
    {
        //[Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(150, ErrorMessage = "max length is 150 character")]
        public string Name { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "max length is 200 character")]
        public string Vision { get; set; }



        [Required]
        [MaxLength(150, ErrorMessage = "max length is 200 character")]
        public string Mission { get; set; }



        [Required]
        [MaxLength(150, ErrorMessage = "max length is 150 character")]
        public string HeadQuarter { get; set; }



    }





}
