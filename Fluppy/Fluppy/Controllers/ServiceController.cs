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
   
    public class ServiceController : Controller
    {
        // GET: Service

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            VmService model = new VmService();

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

            model.Services = db.Services.Include("Appointments").ToList();
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "Service");

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: ServiceDetails

        public ActionResult ServiceDetails(int Id)
        {
            VmService model = new VmService();

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

            model.Service = db.Services.Include("Appointments").FirstOrDefault(d=>d.Id==Id);
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "ServiceDetails");

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // POST: ServiceMessage

        [HttpPost]
        public ActionResult ServiceMessage(VmService message)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = new Appointment();
                
                appointment.CreatedDate = DateTime.UtcNow.AddHours(4);
                appointment.DateAndTime = DateTime.ParseExact(message.Appointment.DateAndTimeNotMapped, "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                appointment.Email = message.Appointment.Email;
                appointment.Name = message.Appointment.Name;
                appointment.Message = message.Appointment.Message;
                appointment.Phone = message.Appointment.Phone;
                appointment.ServiceId = message.ServiceId;

                db.Appointments.Add(appointment);
                db.SaveChanges();

                Session["Success"] = true;
            }
            else
            {
                Session["Required"] = true;
            }
            return RedirectToAction("ServiceDetails", new { Id = message.ServiceId });
        }
    }
}