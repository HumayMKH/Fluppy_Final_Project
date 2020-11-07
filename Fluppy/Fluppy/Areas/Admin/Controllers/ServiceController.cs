using Fluppy.Areas.Admin.Filters;
using Fluppy.DAL;
using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Areas.Admin.Controllers
{
    [logout]
    public class ServiceController : Controller
    {
        // GET: Admin/Service

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<Service> services = db.Services.ToList();

            return View(services);
        }

        // GET: Admin/Service/Details

        public ActionResult Details(int Id)
        {
            Service service = db.Services.Find(Id);

            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Admin/Service/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Service/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                Service Service = new Service();

                if (service.PriceDayBig <= 0  || service.PriceDaySmall <= 0 || service.PriceMonthBig <= 0
                    || service.PriceMonthSmall <= 0 || service.PriceWeekBig <= 0 || service.PriceWeekSmall <= 0)
                {
                    ModelState.AddModelError("", "You cannot type minus numbers!");

                    return View(service);
                }

                if (service.ImageFile == null)
                {
                    ModelState.AddModelError("", "Image is required");

                    return View(service);
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + service.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    service.ImageFile.SaveAs(imagePath);
                    Service.Image = imageName;
                }
                Service.Title = service.Title;
                Service.Content = service.Content;
                Service.PriceDayBig = service.PriceDayBig;
                Service.PriceDaySmall = service.PriceDaySmall;
                Service.PriceMonthBig = service.PriceMonthBig;
                Service.PriceMonthSmall = service.PriceMonthSmall;
                Service.PriceWeekBig = service.PriceWeekBig;
                Service.PriceWeekSmall = service.PriceWeekSmall;

                db.Services.Add(Service);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Admin/Service/Update
        public ActionResult Update(int id)
        {
            Service service = db.Services.Find(id);

            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Admin/Service/Update

        [HttpPost]
        public ActionResult Update(Service service)
        {
            if (ModelState.IsValid)
            {
                Service Service = db.Services.Find(service.Id);

                if (service.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + service.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string OldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Service.Image);

                    service.ImageFile.SaveAs(imagePath);
                    System.IO.File.Delete(OldImagePath);
                    
                    Service.Image = imageName;
                }
                Service.Title = service.Title;
                Service.Content = service.Content;
                Service.PriceDayBig = service.PriceDayBig;
                Service.PriceDaySmall = service.PriceDaySmall;
                Service.PriceMonthBig = service.PriceMonthBig;
                Service.PriceMonthSmall = service.PriceMonthSmall;
                Service.PriceWeekBig = service.PriceWeekBig;
                Service.PriceWeekSmall = service.PriceWeekSmall;
                
                db.Entry(Service).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Admin/Service/Delete

        public ActionResult Delete(int id)
        {
            Service service = db.Services.Find(id);

            if(service == null)
            {
               return HttpNotFound();
            }
            db.Services.Remove(service);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}