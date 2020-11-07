using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Title { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageBig { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFileBig { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public int AdminId { get; set; }
        public Admins Admin { get; set; }
        public List<Comment> Comments { get; set; }
    }
}