using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fluppy.ViewModels
{
    public class VmBlog
    {
        public List<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public HeaderImage HeaderImage { get; set; }
        public List<Adopt> Adopts { get; set; }
        public List<BlogCategory>  BlogCategories { get; set; }
        public int PageCount { get; set; }
        public int Currentpage { get; set; }
    }
}