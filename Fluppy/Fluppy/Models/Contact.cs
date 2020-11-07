using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage ="The field is required")]
        public string Fullname { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "The field is required")]
        public string Phone { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "The field is required")]
        public string Email { get; set; }
        [MaxLength(500)]
        [Required(ErrorMessage = "The field is required")]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}