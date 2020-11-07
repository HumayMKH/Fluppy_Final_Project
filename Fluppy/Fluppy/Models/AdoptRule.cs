using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class AdoptRule
    {
        public int Id { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
    }
}