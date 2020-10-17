using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }

        [Required]
        public int DojoActivityId { get; set; }
        public DojoActivity DojoActivity { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
