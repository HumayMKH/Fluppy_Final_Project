using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class PetType
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public List<PetTypeToProduct> PetTypeToProducts { get; set; }

    }
}