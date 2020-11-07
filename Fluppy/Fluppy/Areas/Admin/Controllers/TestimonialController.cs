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
    public class TestimonialController : Controller
    {
        // GET: Admin/Testimonial

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<Testimonial> testimonials = db.Testimonials.ToList();

            return View(testimonials);
        }

        // GET: Admin/Testimonial/Details

        public ActionResult Details(int Id)
        {
            Testimonial testimonial = db.Testimonials.Find(Id);

            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // GET: Admin/Testimonial/Create

        public ActionResult Create()
        {
                return View();
        }

        // POST: Admin/Testimonial/Create

        [HttpPost]
        public ActionResult Create(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                if (testimonial.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + testimonial.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    testimonial.ImageFile.SaveAs(imagePath);
                    testimonial.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View(testimonial);
                }
                testimonial.CreatedDate = DateTime.Now;

                db.Testimonials.Add(testimonial);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(testimonial);
        }

        // GET: Admin/Testimonial/Update

        public ActionResult Update(int id)
        {
            Testimonial testimonial = db.Testimonials.Find(id);

            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Admin/Testimonial/Update

        [HttpPost]
        public ActionResult Update(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                Testimonial Testimonial = db.Testimonials.Find(testimonial.Id);

                if (testimonial.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + testimonial.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Testimonial.Image);
                    System.IO.File.Delete(oldImagePath);

                    testimonial.ImageFile.SaveAs(imagePath);
                    Testimonial.Image = imageName;
                }
                Testimonial.Fullname = testimonial.Fullname;
                Testimonial.Content = testimonial.Content;
                Testimonial.Position = testimonial.Position;

                db.Entry(Testimonial).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(testimonial);
        }

        // GET: Admin/Testimonial/Delete

        public ActionResult Delete(int id)
        {
            Testimonial testimonial = db.Testimonials.Find(id);

            if (testimonial == null)
            {
                return HttpNotFound();
            }
            db.Testimonials.Remove(testimonial);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}