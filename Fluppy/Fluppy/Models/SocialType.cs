using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class SocialType
    {
        public int Id { get; set; }
        [MaxLength(15)]
        [Required]
        public string Title { get; set; }
        [MaxLength(20)]
        [Required]
        public string Icon { get; set; }
        public List<HomeSocial> HomeSocials { get; set; }
        public List<TeamSocial> TeamSocials { get; set; }
        public List<AdoptSocial> AdoptSocials { get; set; }

    }
}