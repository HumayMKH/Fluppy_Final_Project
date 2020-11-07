using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [MaxLength(500)]
        [Required]
        public string Content { get; set; }
        [MaxLength(50)]
        [Required]
        public string Fullname { get; set; }
        [MaxLength(50)]
        [Required]
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}