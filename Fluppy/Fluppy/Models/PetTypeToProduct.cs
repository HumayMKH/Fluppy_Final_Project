using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class PetTypeToProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }
    }
}