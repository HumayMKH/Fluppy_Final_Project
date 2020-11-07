using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmSearchShop
    {
        public int? CatId { get; set; }
        public int? PetTypeId { get; set; }
        public int? PriceMax { get; set; }
        public int? PriceMin { get; set; }
        public string Search { get; set; }
    }
}