using Fluppy.DAL;
using Fluppy.Models;
using Fluppy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Controllers
{
    public class AdoptController : Controller
    {
        // GET: Adopt

        FluppyContext db = new FluppyContext();

        [ValidateInput(false)]
        public ActionResult Index(int page=1)
        {
            VmAdopt model = new VmAdopt();

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

            model.Adopts = db.Adopts.Include("Gender")
                                    .Include("PetSize")
                                    .Include("AgeType")
                                    .Include("AdoptSocials")
                                    .Include("AdoptSocials.SocialType")
                                    .Include("SlideAdopts").OrderBy(o => o.Id).Skip((page-1)*8).Take(8).ToList();
            model.SlideAdopts = db.SlideAdopts.ToList();
            model.Articles = db.Articles.Include("Admin").ToList();
            model.AdoptRule = db.AdoptRules.FirstOrDefault();
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "Adopt");
            ViewBag.HomeSetting = db.HomeSettings.Include("HomeSocials")
                                                 .Include("HomeSocials.SocialType").FirstOrDefault();

            model.PageCount = Convert.ToInt32(Math.Ceiling(db.Blogs.Count() / 4.0));
            model.Currentpage = page;

            return View(model);
        }

        // GET: AdoptDetails

        public ActionResult AdoptDetails(int? Id)
        {
            VmAdopt model = new VmAdopt();

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
           
            model.Adopt = db.Adopts.Include("Gender")
                                        .Include("Appointments")
                                        .Include("PetSize")
                                        .Include("AgeType")
                                        .Include("AdoptSocials")
                                        .Include("AdoptSocials.SocialType")
                                        .Include("SlideAdopts").FirstOrDefault(b => b.Id == Id);
            model.SlideAdopts = db.SlideAdopts.Where(i=>i.AdoptId==Id).ToList();
            model.Articles = db.Articles.Include("Admin").ToList();
            model.AdoptRule = db.AdoptRules.FirstOrDefault();
            ViewBag.HomeSetting = db.HomeSettings.Include("HomeSocials")
                                                 .Include("HomeSocials.SocialType").FirstOrDefault();
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "AdoptDetails");

            return View(model);
        }

        // GET: AdoptMessage

        [HttpPost]
        public ActionResult AdoptMessage(VmAdopt message)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = new Appointment();

                appointment.CreatedDate = DateTime.UtcNow.AddHours(4);
                appointment.DateAndTime = DateTime.ParseExact(message.Appointment.DateAndTimeNotMapped, "dd.MM.yyyy HH:mm",System.Globalization.CultureInfo.InvariantCulture);
                appointment.Email = message.Appointment.Email;
                appointment.Name = message.Appointment.Name;
                appointment.Message = message.Appointment.Message;
                appointment.Phone = message.Appointment.Phone;
                appointment.AdoptId = message.AdoptId;

                db.Appointments.Add(appointment);
                db.SaveChanges();

                Session["Success"] = true;
            }
            else
            {
                Session["Required"] = true;
            }

            return RedirectToAction("AdoptDetails", new {Id = message.AdoptId});
        }
    }
}