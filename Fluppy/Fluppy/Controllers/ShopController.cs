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
    public class ShopController : Controller
    {
        FluppyContext db = new FluppyContext();

        // GET: Checkout

        public ActionResult Checkout()
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

            VmProduct model = new VmProduct();

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }

            model.Products = products;
            model.HeaderImage = db.HeaderImages.FirstOrDefault(i => i.Page == "Checkout");

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: ProductDetails

        public ActionResult ProductDetails(int? Id)
        {
            VmProduct model = new VmProduct();

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
            if (cookieCart != null)
            {
                List<string> CartList = cookieCart.Value.Split(',').ToList();

                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }
            #endregion

            Product product = db.Products
                              .Include("Admin")
                              .Include("ProductCategory")
                              .Include("ProductImages")
                              .FirstOrDefault(p => p.Id == Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            model.Product = product;
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "ProductDetails");
            model.Products = db.Products
                             .Include("Admin")
                             .Include("ProductCategory")
                             .Include("ProductImages")
                             .Where(p =>p.ProductCategoryId == product.ProductCategoryId)
                             .OrderByDescending(o=>o.Id).ToList();
            model.ProductImages = db.ProductImages.Where(i => i.ProductId == Id).ToList();
            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: WishList

        public ActionResult WishList()
        {
            #region Cart list
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieCart != null)
            {
                List<string> CartList = cookieCart.Value.Split(',').ToList();

                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }
            #endregion

            VmProduct model = new VmProduct();
            if (model != null)
            {
                List<string> WishList = Request.Cookies["WishList"].Value.Split(',').ToList();
                WishList.RemoveAt(WishList.Count - 1);
                ViewBag.WishListCount = WishList.Count;

                model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "WishList");
                model.Products = db.Products
                                 .Include("ProductImages")
                                 .Where(p => WishList.Contains(p.Id.ToString()) == true).ToList();
            }
            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: AddToWishList

        public JsonResult AddToWishList(int? id)
        {
            string response = "";

            if (id != null)
            {
                if (Request.Cookies["WishList"] != null)
                {
                    string oldList = Request.Cookies["WishList"].Value;
                    HttpCookie cookie = new HttpCookie("WishList");
                    cookie.Value = oldList;

                    if (!oldList.Contains(id.ToString()))
                    {
                        cookie.Value += id + ",";
                        Request.Cookies["WishList"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "success-true";
                    }
                    else
                    {
                        List<string> oldListArr = oldList.Split(',').ToList();
                        oldListArr.Remove(id.ToString());
                        oldList = string.Join(",", oldListArr);
                        cookie.Value = oldList;
                        Request.Cookies["WishList"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "success-false";
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("WishList");

                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Value += id + ",";
                    Response.Cookies.Add(cookie);
                    response = "success-true";
                }
            }
            else
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: AddToCart
        public JsonResult AddToCart(int? id, decimal? count)
        {
            string response = "";
            if (id != null && count != null)
            {
                if (Request.Cookies["Cart"] != null)
                {
                    string oldList = Request.Cookies["Cart"].Value;
                    HttpCookie cookie = new HttpCookie("Cart");
                    cookie.Value = oldList;
                    List<string> cartList = oldList.Split(',').ToList();
                    cartList.RemoveAt(cartList.Count - 1);

                    string cartElement = cartList.FirstOrDefault(c => Convert.ToInt32(c.Split('-')[0]) == id);

                    if (cartElement == null)
                    {
                        cookie.Value += id + "-" + count + ",";
                        Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "success-true";
                    }
                    else
                    {
                        cartList.Remove(cartElement);

                        if (cartList.Count() > 0)
                        {
                            cookie.Value = string.Join(",", cartList) + ",";
                            cookie.Value += id + "-" + count + ",";
                        }
                        else
                        {
                            cookie.Value = id + "-" + count + ",";
                        }

                        Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "success-false";
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("Cart");

                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Value += id + "-" + count + ",";
                    Response.Cookies.Add(cookie);
                    response = "success-true";
                }
            }
            else
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Proceed

        public ActionResult Proceed()
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

            VmProceed model = new VmProceed();

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            model.Products = products;
            model.HeaderImage = db.HeaderImages.FirstOrDefault(i => i.Page == "Proceed");

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // POST: Proceed

        [HttpPost]
        public ActionResult Proceed(VmProceed proceed)
        {
            //Get money - Sent data to API
            //Call API
            string APIResponse = "OK";


            //Next Steps
            if (APIResponse == "OK")
            {
                //Add Client
                int? ClientId=null;
                if (Session["LoginnedUser"] == null)
                {
                    if (ModelState.IsValid)
                    {
                        User client = new User();
                        client.Name = proceed.Name;
                        client.Surname = proceed.Surname;
                        client.Email = proceed.Email;
                        client.Phone = proceed.Phone;
                        client.Address = proceed.Address;
                        client.City = proceed.City;
                        client.PostCode = proceed.PostCode;
                        client.Notes = proceed.Notes;
                        client.CreatedDate = DateTime.Now;
                        client.IsRegistered = false;

                        db.Users.Add(client);
                        db.SaveChanges();
                        ClientId = client.Id;
                    }
                }
                else
                {
                    ClientId = (int)Session["LoginnedUserId"];
                }


                //Sell product
                foreach (var item in proceed.ProductId)
                {
                    Product product = db.Products.Find(item);
                    int index = proceed.ProductId.ToList().IndexOf(item);
                    if (product.Quantity < proceed.ProductCount[index])
                    {
                        Session["PrdIsNotEnough"] = true;
                        goto Get;
                    }
                    Sale sale = new Sale();
                    sale.ProductId = item;
                    sale.Quantity = proceed.ProductCount[index];
                    sale.UserId =(int) ClientId;
                    sale.CreatedDate = DateTime.Now;
                    sale.Price = product.Price;

                    db.Sales.Add(sale);

                    product.Quantity -= proceed.ProductCount[index];
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
                Session["Success"] = true;

                HttpCookie cookieCartMe = new HttpCookie("Cart");
                cookieCartMe.Value = "";
                cookieCartMe.Expires = DateTime.Now.AddDays(-1);

                HttpCookie cookieWishMe = new HttpCookie("WishList");
                cookieWishMe.Value = "";
                cookieWishMe.Expires = DateTime.Now.AddDays(-1);

                Response.Cookies.Add(cookieCartMe);
                Response.Cookies.Add(cookieWishMe);

                return RedirectToAction("Checkout");
            }
            else
            {
                Session["MoneyIsNotEnough"] = true;
                goto Get;
            }

            Get:
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

            VmProceed model = new VmProceed();

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            model.Products = products;
            model.HeaderImage = db.HeaderImages.FirstOrDefault(i => i.Page == "Checkout");

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            return View(model);
        }

        // GET: RemoveFromCart

        public JsonResult RemoveFromCart(int? id)
        {
            string response = "";

            if (id != null)
            {
                string oldList = Request.Cookies["Cart"].Value;
                HttpCookie cookie = new HttpCookie("Cart");
                List<string> cartList = oldList.Split(',').ToList();
                cartList.RemoveAt(cartList.Count - 1);

                string cartElement = cartList.FirstOrDefault(c => Convert.ToInt32(c.Split('-')[0]) == id);
                if (cartElement != null)
                {
                    cartList.Remove(cartElement);

                    if (cartList.Count() > 0)
                    {
                        cookie.Value = string.Join(",", cartList) + ",";
                    }
                    else
                    {
                        cookie.Value = "";
                    }

                    Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                    response = "success-true";
                }

            }
            else
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}
