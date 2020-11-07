using Fluppy.DAL;
using Fluppy.Models;
using Fluppy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Controllers
{
    public class BlogController : Controller
    {
        FluppyContext db = new FluppyContext();

        // GET: Blog

        public ActionResult Index(string searchText, int? CatId, int page = 1)
        {
            VmBlog model = new VmBlog();

            #region Wish list
            HttpCookie cookie = Request.Cookies["WishList"];
            if (cookie != null)
            {
                List<string> WishList = cookie.Value.Split(',').ToList();

                WishList.RemoveAt(WishList.Count - 1);

                ViewBag.WishList = WishList;
                ViewBag.WishListCount = WishList.Count;
            }
            else
            {
                ViewBag.WishListCount = 0;
            }
            #endregion

            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }
            #endregion

            List<Blog> blogs = db.Blogs.Include("BlogCategory").Include("Admin")
                               .Where(w => (CatId != null ? w.BlogCategoryId == CatId : true) &&
                               ((!string.IsNullOrEmpty(searchText) ? w.Title.Contains(searchText) : true) ||
                               (!string.IsNullOrEmpty(searchText) ? w.Content.Contains(searchText) : true))).ToList();

            model.Blogs = blogs.OrderByDescending(o => o.Id).Skip((page - 1) * 4).Take(4).ToList();

            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "Blog");
            model.Adopts = db.Adopts.Include("Gender")
                                    .Include("PetSize")
                                    .Include("AgeType")
                                    .Include("AdoptSocials")
                                    .Include("AdoptSocials.SocialType")
                                    .Include("SlideAdopts")
                                    .OrderByDescending(o => o.Id).Take(3).ToList();
            model.BlogCategories = db.BlogCategories.ToList();
            ViewBag.HomeSetting = db.HomeSettings.Include("HomeSocials")
                                                 .Include("HomeSocials.SocialType").FirstOrDefault();

            model.PageCount = Convert.ToInt32(Math.Ceiling(blogs.Count() / 4.0));
            model.Currentpage = page;

            return View(model);
        }

        // GET: BlogDetails

        public ActionResult BlogDetails(int Id,Blog blog)
        {

            #region Wish list
            HttpCookie cookie = Request.Cookies["WishList"];
            if (cookie != null)
            {
                List<string> WishList = cookie.Value.Split(',').ToList();

                WishList.RemoveAt(WishList.Count - 1);

                ViewBag.WishList = WishList;
                ViewBag.WishListCount = WishList.Count;
            }
            else
            {
                ViewBag.WishListCount = 0;
            }
            #endregion

            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }
            #endregion

            VmBlog model = new VmBlog();
            model.Blog = db.Blogs.Include("BlogCategory").Include("Admin").FirstOrDefault(b => b.Id == Id);
            Blog Blog = db.Blogs.Find(blog.Id);

            model.Blog.ViewCount++;

            db.Entry(Blog).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.HomeSetting = db.HomeSettings.Include("HomeSocials")
                                                 .Include("HomeSocials.SocialType").FirstOrDefault();
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "BlogDetails");

            return View(model);
        }
    }
}