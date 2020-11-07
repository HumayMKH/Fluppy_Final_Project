using Fluppy.Areas.Admin.Filters;
using Fluppy.DAL;
using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Areas.Admin.Controllers
{
    [logout]
    public class ProductController : Controller
    {
        // GET: Admin/Product

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            List<Product> products = db.Products
                                     .Include("Admin")
                                     .Include("ProductCategory")
                                     .Include("ProductImages")
                                     .Include("PetTypeToProducts")
                                     .Include("PetTypeToProducts.PetType")
                                     .Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.Price.ToString().Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.Admin.Name.Contains(searchText) : true)).ToList();
            return View(products);
        }

        // GET: Admin/Product/Details

        public ActionResult Details(int id)
        {
            Product product = db.Products
                              .Include("Admin")
                              .Include("ProductCategory")
                              .Include("ProductImages")
                              .Include("PetTypeToProducts")
                              .Include("PetTypeToProducts.PetType")
                              .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create

        public ActionResult Create()
        {
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.PetType = db.PetTypes.ToList();

            return View();
        }

        // POST: Admin/Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                Product Product = new Product();
                Product.Quantity = product.Quantity;
                Product.AdminId = (int)Session["LoginnerId" +
                    ""];
                Product.Desc = product.Desc;
                Product.Name = product.Name;
                Product.Price = product.Price;
                Product.CreatedDate = DateTime.Now;
                Product.ProductCategoryId = product.ProductCategoryId;

                db.Products.Add(Product);
                db.SaveChanges();

                foreach (var item in product.PetTypeId)
                {
                    PetTypeToProduct petTypeToProduct = new PetTypeToProduct();
                    petTypeToProduct.ProductId = Product.Id;
                    petTypeToProduct.PetTypeId = item;

                    db.PetTypeToProducts.Add(petTypeToProduct);
                    db.SaveChanges();
                }

                foreach (HttpPostedFileBase image in product.ImageFile)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + image.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                    image.SaveAs(imagePath);

                    ProductImage productImage = new ProductImage();
                    productImage.Image = imageName;
                    productImage.ProductId = Product.Id;

                    db.ProductImages.Add(productImage);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.PetType = db.PetTypes.ToList();

            return View(product);
        }

        // GET: Admin/Product/Update

        public ActionResult Update(int id)
        {
            Product product = db.Products.Include("ProductImages").FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.PetType = db.PetTypes.ToList();

            return View(product);
        }

        // POST: Admin/Product/Update

        [HttpPost]
        public ActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                Product Product = db.Products.Include("ProductImages").Include("PetTypeToProducts").FirstOrDefault(p => p.Id == product.Id);

                if (product.ImageFile.Length > 0)
                {
                    foreach (ProductImage item in Product.ProductImages.ToList())
                    {
                        string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), item.Image);
                        System.IO.File.Delete(oldImagePath);

                        db.ProductImages.Remove(item);
                        db.SaveChanges();
                    }

                    foreach (HttpPostedFileBase image in product.ImageFile)
                    {
                        string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + image.FileName;
                        string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                        image.SaveAs(imagePath);
                        ProductImage productImage = new ProductImage();
                        productImage.Image = imageName;
                        productImage.ProductId = product.Id;

                        db.ProductImages.Add(productImage);
                        db.SaveChanges();
                    }
                }
                if (product.PetTypeId.Length > 0)
                {
                    foreach (PetTypeToProduct item in Product.PetTypeToProducts.ToList())
                    {
                        db.PetTypeToProducts.Remove(item);
                        db.SaveChanges();
                    }
                    foreach (int item in product.PetTypeId)
                    {
                        PetTypeToProduct petTypeToProduct = new PetTypeToProduct();
                        petTypeToProduct.ProductId = Product.Id;
                        petTypeToProduct.PetTypeId = item;

                        db.PetTypeToProducts.Add(petTypeToProduct);
                        db.SaveChanges();
                    }
                }
                Product.Name = product.Name;
                Product.Price = product.Price;
                Product.Desc = product.Desc;
                Product.Quantity = product.Quantity;
                Product.Name = product.Name;

                db.Entry(Product).State = EntityState.Modified;
                db.Entry(Product).Property(c => c.CreatedDate).IsModified = false;
                db.Entry(Product).Property(c => c.AdminId).IsModified = false;
                db.Entry(Product).Property(c => c.ProductCategoryId).IsModified = false;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            ViewBag.PetType = db.PetTypes.ToList();

            return View(product);
        }

        // GET: Admin/Product/Delete

        public ActionResult Delete(int id)
        {
            Product product = db.Products
                              .Include("ProductImages")
                              .Include("ProductCategory")
                              .Include("PetTypeToProducts")
                              .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            foreach (var item in db.PetTypeToProducts.ToList())
            {
                if (item.ProductId == id)
                {
                    db.PetTypeToProducts.Remove(item);
                }
            }
            foreach (var item1 in db.Sales.ToList())
            {
                if (item1.ProductId == id)
                {
                    db.Sales.Remove(item1);
                }
            }

            foreach (var item in db.ProductImages.ToList())
            {
                if (item.ProductId == id)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads"), item.Image);
                    
                    System.IO.File.Delete(imagePath);
                    db.ProductImages.Remove(item);
                }
            }

            db.SaveChanges();

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Product/JsonResult GetPetTypes

        public JsonResult GetPetTypes()
        {
            var categories = db.PetTypes.Select(c => new {
                c.Id,
                c.Type
            }).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}
         