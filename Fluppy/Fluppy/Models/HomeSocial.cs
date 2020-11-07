using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.Models
{
    public class HomeSocial
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Link { get; set; }
        public int SocialTypeId { get; set; }
        public SocialType SocialType { get; set; }
        public int HomeSettingId { get; set; }
        public HomeSetting HomeSetting { get; set; }
    }
}