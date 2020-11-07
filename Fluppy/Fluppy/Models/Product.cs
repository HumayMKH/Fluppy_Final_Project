using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Column(TypeName ="money")]
        [Required]
        public decimal Price { get; set; }
        [MaxLength(300)]
        [Required]
        public string Desc { get; set; }
        [NotMapped]
        public decimal Count { get; set; }
        [Column(TypeName ="money")]
        public decimal Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int AdminId { get; set; }
        public Admins Admin { get; set; }
        public List<PetTypeToProduct> PetTypeToProducts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Sale> Sales { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] ImageFile { get; set; }
        [NotMapped]
        public int[] PetTypeId { get; set; }

    }
}