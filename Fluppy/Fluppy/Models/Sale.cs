using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Sale
    {
        public int Id { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Quantity { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}