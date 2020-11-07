using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmAbout
    {
        public HeaderImage HeaderImage { get; set; }
        public List<Service> Services { get; set; }
        public List<Article> Articles { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Team> Teams { get; set; }
        public List<Adopt> Adopts { get; set; }
    }
}