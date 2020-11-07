using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(30)]
        [Required]
        public string Phone { get; set; }
        [MaxLength(30)]
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateAndTime { get; set; }
        [NotMapped]
        public string DateAndTimeNotMapped { get; set; }
        [MaxLength(500)]
        [Required]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ServiceId { get; set; }
        public Service Service { get; set; }
        public int? AdoptId { get; set; }
        public Adopt Adopt { get; set; }
    }
}