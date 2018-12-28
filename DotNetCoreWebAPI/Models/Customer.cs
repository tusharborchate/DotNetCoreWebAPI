using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name")]
        public String CustomerName { get; set; }

        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the email")]
        public String Email { get; set; }

        [MinLength(4)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the password")]
        public String Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the weight")]
        public double Weight { get; set; }


    }
}



