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
    public class HeaderImageController : Controller
    {
        // GET: Admin/HeaderImage

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<HeaderImage> images = db.HeaderImages.ToList();

            return View(images);
        }

        // GET: Admin/HeaderImage/Details

        public ActionResult Details(int Id)
        {
            HeaderImage image = db.HeaderImages.Find(Id);

            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Admin/HeaderImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HeaderImage/Create

        [HttpPost]
        public ActionResult Create(HeaderImage images)
        {
            if (ModelState.IsValid)
            {
                if (images.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + images.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    images.ImageFile.SaveAs(imagePath);
                    images.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View(images);
                }
                db.HeaderImages.Add(images);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Admin/HeaderImage/Update

        public ActionResult Update(int Id)
        {
            HeaderImage images = db.HeaderImages.Find(Id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Admin/HeaderImage/Update

        [HttpPost]
        public ActionResult Update(HeaderImage images)
        {
            if (ModelState.IsValid)
            {
                HeaderImage Images = db.HeaderImages.Find(images.Id);

                if (images.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + images.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Images.Image);
                    System.IO.File.Delete(oldImagePath);

                    images.ImageFile.SaveAs(imagePath);
                    Images.Image = imageName;
                }
                Images.Title = images.Title;
                Images.Page = images.Page;

                db.Entry(Images).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Admin/HeaderImage/Delete

        public ActionResult Delete(int Id)
        {
            HeaderImage images = db.HeaderImages.Find(Id);

            if (images == null)
            {
                return HttpNotFound();
            }
            db.HeaderImages.Remove(images);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}