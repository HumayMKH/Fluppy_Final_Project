using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class AgeType
    {
        public int Id { get; set; }
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }
        public List<Adopt> Adopts { get; set; }
    }
}