using Fluppy.DAL;
using Fluppy.Models;
using Fluppy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Fluppy.Controllers
{
    public class HomeController : Controller
    {
        // GET: PageNotFound

        FluppyContext db = new FluppyContext();

        public ActionResult PageNotFound()
        {
            return View();
        }

        // GET: Home

        public ActionResult Index(string searchText)
        {
            VmHome model = new VmHome();

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

            model.Products = db.Products
                             .Include("ProductImages")
                             .Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                             || (!string.IsNullOrEmpty(searchText) ? w.Desc.Contains(searchText) : true))
                             .OrderByDescending(o => o.Id).Take(6).ToList();
            model.HomeSliders = db.HomeSliders.ToList();
            model.Services = db.Services
                             .Where(w => (!string.IsNullOrEmpty(searchText) ? w.Title.Contains(searchText) : true)
                             || (!string.IsNullOrEmpty(searchText) ? w.Content.Contains(searchText) : true)).ToList(); 
            model.Blogs = db.Blogs
                          .Include("BlogCategory")
                          .Include("Admin").
                          Where(w => (!string.IsNullOrEmpty(searchText) ? w.Title.Contains(searchText) : true)
                          || (!string.IsNullOrEmpty(searchText) ? w.Content.Contains(searchText) : true))
                          .OrderByDescending(o => o.Id).Take(2).ToList();
            model.Testimonials = db.Testimonials.ToList();
            model.Adopts = db.Adopts
                           .Include("Gender")
                           .Include("PetSize")
                           .Include("AgeType")
                           .Include("AdoptSocials")
                           .Include("AdoptSocials.SocialType")
                           .Include("SlideAdopts").
                           Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                           || (!string.IsNullOrEmpty(searchText) ? w.Address.Contains(searchText) : true)
                           || (!string.IsNullOrEmpty(searchText) ? w.Breed.Contains(searchText) : true))
                           .OrderByDescending(o => o.Id).Take(4).ToList();
           
            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: Login

        public ActionResult Login()
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

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View();
        }

        // POST: Login 

        [HttpPost]
        public ActionResult Login(VmLogin login)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(a => a.Username == login.Username);

                if (user != null)
                {
                    if (Crypto.VerifyHashedPassword(user.Password, login.Password) == true)
                    {
                        Session["LoginnedUser"] = user;
                        Session["LoginnedUserId"] = user.Id;

                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Incorrect Password!");

                        return View(login);
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "Incorrect Username!");

                    return View(login);
                }
            }

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

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(login);
        }

        // GET: Register

        public ActionResult Register()
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

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View();
        }

        // POST: Register

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Username == null)
                {
                    ModelState.AddModelError("Username", "The Username field is required.");

                    return View(user);
                }
                if (user.Password == null)
                {
                    ModelState.AddModelError("Password", "The Password field is required.");

                    return View(user);
                }
                if (db.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This Email is already in use!");

                    return View(user);
                }

                if (db.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "This Username is already in use!");

                    return View(user);
                }
                user.IsRegistered = true;
                user.CreatedDate = DateTime.Now;
                user.Password = Crypto.HashPassword(user.Password);

                db.Users.Add(user);
                db.SaveChanges();

                Session["LoginnedUser"] = user;
                Session["LoginnedUserId"] = user.Id;

                return RedirectToAction("Index", "Home");
            }
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

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(user);
        }

        // GET: Logout

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        // POST: Subscribe

        [HttpPost]
        public JsonResult Subscribe(Subscribe subscribe)
        {
            string response = "";

            if (ModelState.IsValid)
            {
                Subscribe Subscribe = new Subscribe();

                if (db.Subscribes.Any(a => a.Email == subscribe.Email))
                {
                    response = "already";
                }
                else
                {
                    Subscribe.CreatedDate = DateTime.Now;
                    Subscribe.Email = subscribe.Email;

                    db.Subscribes.Add(Subscribe);
                    db.SaveChanges();
                    response = "true";
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
             response ="false";
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}