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
    public class ContactController : Controller
    {
        // GET: Contact

        FluppyContext db = new FluppyContext();
        public ActionResult Index()
        {
            VmContact model = new VmContact();

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

            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "Contact");
            model.Contact = new Contact();
            ViewBag.HomeSetting = db.HomeSettings.Include("HomeSocials")
                                                 .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: UserMessage

        [HttpPost]
        public ActionResult UserMessage(VmContact message)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact
                {
                    CreatedDate = DateTime.UtcNow.AddHours(4),
                    Email = message.Contact.Email,
                    Fullname = message.Contact.Fullname,
                    Message = message.Contact.Message,
                    Phone = message.Contact.Phone
                };
                db.Contacts.Add(contact);
                db.SaveChanges();
           
                Session["Success"] = true;
            }
            else
            {
                Session["Required"] = true;
            }

            return RedirectToAction("Index");
        }
    }
}