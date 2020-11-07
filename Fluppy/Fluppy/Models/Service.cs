using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Service
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [MaxLength(300)]
        [Required]
        public string Content { get; set; }
        [Column(TypeName ="money")]
        public decimal PriceDayBig { get; set; }
        [Column(TypeName = "money")]
        public decimal PriceWeekBig { get; set; }
        [Column(TypeName = "money")]
        public decimal PriceMonthBig { get; set; }
        [Column(TypeName = "money")]
        public decimal PriceDaySmall { get; set; }
        [Column(TypeName = "money")]
        public decimal PriceWeekSmall { get; set; }
        [Column(TypeName = "money")]
        public decimal PriceMonthSmall { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}