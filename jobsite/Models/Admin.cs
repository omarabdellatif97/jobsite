using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jobsite.Models
{
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





}
