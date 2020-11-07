using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmService
    {
        public List<Service> Services { get; set; }
        public Service Service { get; set; }
        public HeaderImage HeaderImage { get; set; }
        public Appointment Appointment { get; set; }
        public int ServiceId { get; set; }
    }
}