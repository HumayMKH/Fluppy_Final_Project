using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Adopt
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [Column(TypeName ="bit")]
        public bool Neutered { get; set; }
        public int Age { get; set; }
        [MaxLength(150)]
        [Required]
        public string Address { get; set; }
        [MaxLength(50)]
        [Required]
        public string Breed { get; set; }
        [Column(TypeName = "bit")]
        public bool Vaccinated { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int PetSizeId { get; set; }
        public PetSize PetSize { get; set; }
        public int AgeTypeId { get; set; }
        public AgeType AgeType { get; set; }
        public List<SlideAdopt> SlideAdopts { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<AdoptSocial> AdoptSocials { get; set; }
        [NotMapped]
        public HttpPostedFileBase[] ImageFile { get; set; }
    }
}