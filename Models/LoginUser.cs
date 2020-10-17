using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        public string LoginEmail { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string LoginPassword { get; set; }
    }
}
