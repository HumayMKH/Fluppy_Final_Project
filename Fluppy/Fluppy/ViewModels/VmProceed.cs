using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmProceed
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Surname { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        [Required, MaxLength(30)]
        public string City { get; set; }
        [Required, MaxLength(150)]
        public string Address { get; set; }

        [Required, MaxLength(10)]
        public string PostCode { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }

        public int[] ProductId { get; set; }

        public decimal[] ProductCount { get; set; }

        public HeaderImage HeaderImage { get; set; }
        public List<Product> Products { get; set; }
    }
}