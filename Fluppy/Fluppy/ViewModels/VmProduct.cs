using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmProduct
    {
        public HeaderImage HeaderImage { get; set; }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<PetType> PetTypes { get; set; }
        public int PageCount { get; set; }
        public int Currentpage { get; set; }
        public VmSearchShop SearchShop { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}