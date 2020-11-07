using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Fluppy.Models;

namespace Fluppy.DAL
{
    public class FluppyContext:DbContext
    {
        public FluppyContext() : base("DB_Fluppy")
        {

        }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Adopt> Adopts { get; set; }
        public DbSet<AdoptRule> AdoptRules { get; set; }
        public DbSet<AdoptSocial> AdoptSocials { get; set; }
        public DbSet<AgeType> AgeTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<HeaderImage> HeaderImages { get; set; }
        public DbSet<HomeSetting> HomeSettings { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<HomeSocial> HomeSocials { get; set; }
        public DbSet<PetSize> PetSizes { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<PetTypeToProduct> PetTypeToProducts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SlideAdopt> SlideAdopts { get; set; }
        public DbSet<SocialType> SocialTypes { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamSocial> TeamSocials { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<User> Users { get; set; }
    }
}