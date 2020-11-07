using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Subscribe
    {
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}