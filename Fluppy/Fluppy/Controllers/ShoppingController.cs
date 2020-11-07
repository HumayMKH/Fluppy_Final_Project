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
    public class ShoppingController : Controller
    {
        FluppyContext db = new FluppyContext();

        // GET: Shopping

        public ActionResult Index(VmSearchShop searchModel, int page=1)
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

            List<Product> products = db.Products
                                     .Include("ProductCategory")
                                     .Include("ProductImages")
                                     .Include("PetTypeToProducts")
                                     .Include("PetTypeToProducts.PetType").ToList()
                                     .Where(p => (searchModel.CatId != null ? p.ProductCategoryId == searchModel.CatId : true) &&
                                     (searchModel.PetTypeId != null ? p.PetTypeId.Any(pt=>pt==searchModel.PetTypeId) : true) &&
                                     ((searchModel.PriceMin != 0 && searchModel.PriceMin != null) ? p.Price >= searchModel.PriceMin : true) &&
                                     ((searchModel.PriceMax != 0 && searchModel.PriceMax != null )? p.Price <= searchModel.PriceMax : true) &&
                                     (searchModel.Search != null ? p.Name.Contains(searchModel.Search) : true)
                                     ).ToList();

            model.Products = products.OrderByDescending(o => o.Id).Skip((page - 1) * 12).Take(12).ToList();
            
            model.Product = db.Products
                            .Include("ProductCategory")
                            .Include("ProductImages")
                            .Include("PetTypeToProducts")
                            .Include("PetTypeToProducts.PetType").FirstOrDefault();
            model.ProductCategories = db.ProductCategories.ToList();
            model.HeaderImage = db.HeaderImages.FirstOrDefault(h => h.Page == "Shop");
            model.PetTypes = db.PetTypes.ToList();
            model.SearchShop = searchModel;

            ViewBag.HomeSetting = db.HomeSettings
                                  .Include("HomeSocials")
                                  .Include("HomeSocials.SocialType").FirstOrDefault();
            model.PageCount = Convert.ToInt32(Math.Ceiling(products.Count() / 12.0));
            model.Currentpage = page;

            return View(model);
        }
    }
}
