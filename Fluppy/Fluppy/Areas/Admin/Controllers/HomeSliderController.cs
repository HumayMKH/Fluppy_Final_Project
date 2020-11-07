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
    public class HomeSliderController : Controller
    {
        // GET: Admin/HomeSlider

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<HomeSlider> homeSliders = db.HomeSliders.ToList();

            return View(homeSliders);
        }

        // GET: Admin/HomeSlider/Details

        public ActionResult Details(int Id)
        {
            HomeSlider homeslider = db.HomeSliders.Find(Id);

            if (homeslider == null)
            {
                return HttpNotFound();
            }
            return View(homeslider);
        }

        // GET: Admin/HomeSlider/Create

        public ActionResult Create()
        {
                return View();
        }

        // POST: Admin/HomeSlider/Create

        [HttpPost]
        public ActionResult Create(HomeSlider homeSlider)
        {
            if (ModelState.IsValid)
            {
                if (homeSlider.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + homeSlider.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    homeSlider.ImageFile.SaveAs(imagePath);
                    homeSlider.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View(homeSlider);
                }
                homeSlider.CreatedDate = DateTime.Now;

                db.HomeSliders.Add(homeSlider);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(homeSlider);
        }

        // GET: Admin/HomeSlider/Update
        public ActionResult Update(int Id)
        {
            HomeSlider homeslider = db.HomeSliders.Find(Id);

            if (homeslider == null)
            {
                return HttpNotFound();
            }
            return View(homeslider);
        }

        // POST: Admin/HomeSlider/Update

        [HttpPost]
        public ActionResult Update(HomeSlider homeSlider)
        {
            if (ModelState.IsValid)
            {
                HomeSlider HomeSlider = db.HomeSliders.Find(homeSlider.Id);
                if (homeSlider.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + homeSlider.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), HomeSlider.Image);
                    System.IO.File.Delete(oldImagePath);

                    homeSlider.ImageFile.SaveAs(imagePath);
                    HomeSlider.Image = imageName;
                }
                HomeSlider.Title = homeSlider.Title;
                HomeSlider.Content = homeSlider.Content;

                db.Entry(HomeSlider).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(homeSlider);
        }

        // GET: Admin/HomeSlider/Delete

        public ActionResult Delete(int Id)
        {
            HomeSlider homeSlider = db.HomeSliders.Find(Id);

            if (homeSlider == null)
            {
                return HttpNotFound();
            }
            db.HomeSliders.Remove(homeSlider);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}