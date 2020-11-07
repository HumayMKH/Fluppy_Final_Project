using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public HeaderImage HeaderImage { get; set; }

    }
}