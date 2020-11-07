using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmAdopt
    {
        public List<Adopt> Adopts { get; set; }
        public Adopt Adopt { get; set; }
        public List<SlideAdopt> SlideAdopts { get; set; }
        public List<Article>  Articles { get; set; }
        public AdoptRule AdoptRule { get; set; }
        public HeaderImage HeaderImage { get; set; }
        public Appointment Appointment { get; set; }
        public int? AdoptId { get; set; }
        public int PageCount { get; set; }
        public int Currentpage { get; set; }
    }
}