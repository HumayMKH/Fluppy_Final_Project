using Fluppy.DAL;
using Fluppy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Controllers
{
    public class AboutController : Controller
    {

        FluppyContext db = new FluppyContext();

        // GET: About

        public ActionResult Index()
        {
            VmAbout model = new VmAbout();

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

            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "About");
            model.Services = db.Services.ToList(); 
            model.Blogs = db.Blogs.Include("BlogCategory")
                                  .Include("Admin").OrderByDescending(o => o.Id).Take(2).ToList();
            model.Teams = db.Teams.Include("TeamSocials")
                                  .Include("TeamSocials.SocialType").Include("Position").ToList();
            model.Articles = db.Articles.Include("Admin").ToList();
            model.Adopts = db.Adopts.Include("Gender")
                                    .Include("PetSize")
                                    .Include("AgeType")
                                    .Include("AdoptSocials")
                                    .Include("SlideAdopts").OrderBy(o => o.Id).Take(4).ToList();
            ViewBag.HomeSetting = db.HomeSettings
                                   .Include("HomeSocials")
                                   .Include("HomeSocials.SocialType").FirstOrDefault();
            ViewBag.Adopt = db.Appointments.Where(a => a.AdoptId != null).Count();
            ViewBag.Service = db.Appointments.Where(a => a.ServiceId != null).Count();
            ViewBag.Contact = db.Sales.Count();

            return View(model);
        }
    }
}