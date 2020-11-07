using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Article
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Title { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AdminId { get; set; }
        public Admins Admin { get; set; }
    }
}