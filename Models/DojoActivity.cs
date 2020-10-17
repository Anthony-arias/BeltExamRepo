using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeltExam.Controllers;

namespace BeltExam.Models
{
    public class DateVaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult("all fields are required"); }
            //string evaluate = (string)value;
            string message = "";
            bool success = true;
            if ((DateTime)value < DateTime.Now.Date)
            {

                //value.AddModelError("Date", "Activity must be in the future!");
                message += "Activity must be in the future!";
                success = false;
                //return View("DojoActivityForm"); //Change this
            }
            if (success) return ValidationResult.Success;
            else return new ValidationResult(message);
        }

    }

    public class DojoActivity
    {
        [Key]
        public int DojoActivityId { get; set; }
        [Required]
        public int UserId { get; set; }

        public User Creator { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Time { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateVa]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public string DurationFormat { get; set; }


        public List<Association> Attendees { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
