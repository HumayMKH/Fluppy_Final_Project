using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class PetSize
    {
        public int Id { get; set; }
        [MaxLength(15)]
        [Required]
        public string Size { get; set; }
        public List<Adopt> Adopts { get; set; }

    }
}