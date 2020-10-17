using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeltExam.Controllers;

namespace BeltExam.Models
{

    public class dateVaAttribute : ValidationAttribute
    {
        private bool containsInt(string input)
        {
            foreach (char val in input)
            {
                if (Char.IsDigit(val)) return true;
            }
            return false;
        }

        private bool containsLetter(string input)
        {
            foreach (char val in input)
            {
                if (Char.IsLetter(val)) return true;
            }
            return false;
        }

        private bool containsSpecial(string input)
        {
            foreach (char val in input)
            {
                if (!Char.IsLetterOrDigit(val)) return true;
            }
            return false;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult("all fields are required"); }
            string evaluate = (string)value;
            string message = "";
            bool success = true;
            if (!containsInt(evaluate)) {message +=" Password must contain at least one number "; success = false; }
            if (!containsLetter(evaluate)) { message +=" Password must contain at least one letter "; success = false; }
            if (!containsSpecial(evaluate)) { message +=" Password must contain at least one special character "; success = false; }

            //success? return ValidationResult.Success: return new ValidationResult(message);
            if (success) return ValidationResult.Success;
            else return new ValidationResult(message);
            /*if (((string)value).ToLower()[0] == 'z')
                return new ValidationResult("No names that start with Z allowed!");
            return ValidationResult.Success;
        }*/
        }

    }
        public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        [dateVa]
        public string Password { get; set; }

        public List<DojoActivity> CreatedDojoActivities { get; set; }
       
        public List<Association> AttendingEvents { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
    }
}
