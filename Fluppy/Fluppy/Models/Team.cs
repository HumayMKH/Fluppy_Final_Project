using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class Team
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Fullname { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public List<TeamSocial> TeamSocials { get; set; }
    }
}