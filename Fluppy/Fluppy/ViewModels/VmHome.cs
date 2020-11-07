using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmHome
    {
        public List<HomeSlider> HomeSliders { get; set; }
        public List<Service> Services { get; set; }
        public List<Product> Products { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Adopt> Adopts { get; set; }
    }
}