using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class HomeSetting
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Logo { get; set; }
        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }
        [MaxLength(150)]
        public string FooterLogo { get; set; }
        [NotMapped]
        public HttpPostedFileBase FooterLogoFile { get; set; }
        [MaxLength(30)]
        [Required]
        public string Email { get; set; }
        [MaxLength(30)]
        [Required]
        public string Phone { get; set; }
        [MaxLength(50)]
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime SatStartTime { get; set; }
        public DateTime SatEndTime { get; set; }
        [MaxLength(150)]
        [Required]
        public string FooterContent { get; set; }
        [MaxLength(100)]
        [Required]
        public string CopyRight { get; set; }
        public List<HomeSocial> HomeSocials { get; set; }
    }
}