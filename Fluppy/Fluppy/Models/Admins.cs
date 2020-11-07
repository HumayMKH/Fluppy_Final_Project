using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Admins
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Surname { get; set; }

        [Required, MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }
        
        [Column(TypeName ="bit")]
        public bool MainAdmin { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Article> Articles { get; set; }
        public List<Blog> Blog { get; set; }

        public List<Product> Products { get; set; }
    }
}