using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        //[Required]
        [MaxLength(80, ErrorMessage = "max length is 80 character")]
        public string Name { get; set; }


        //[Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        //[Required]
        public Gender Gender { get; set; }

        //[Required]
        [MaxLength(150, ErrorMessage = "address can't be more than 150 character")]
        public string Address { get; set; }
    }
}
