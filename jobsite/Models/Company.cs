using System.ComponentModel.DataAnnotations;

namespace jobsite.Models
{
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
