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
    public class AdoptController : Controller
    {
        // GET: Admin/Adopt

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            return View(db.Adopts
                        .Include("Gender")
                        .Include("PetSize")
                        .Include("AgeType")
                        .Include("SlideAdopts")
                        .Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Address.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Breed.Contains(searchText) : true)).ToList());
        }

        // GET: Admin/Adopt/Details

        public ActionResult Details(int id)
        {
            Adopt adopt = db.Adopts.Include("Gender")
                                   .Include("PetSize")
                                   .Include("AgeType")
                                    .Include("SlideAdopts")
                                   .FirstOrDefault(p => p.Id == id);
            if (adopt == null)
            {
                return HttpNotFound();
            }
            return View(adopt);
        }

        // GET: Admin/Adopt/Create

        public ActionResult Create()
        {
            ViewBag.Gender = db.Genders.ToList();
            ViewBag.PetSize = db.PetSizes.ToList();
            ViewBag.AgeType = db.AgeTypes.ToList();

            return View();
        }

        // POST: Admin/Adopt/Create

        [HttpPost]
        public ActionResult Create(Adopt adopt)
        {
            if (ModelState.IsValid)
            {
                Adopt Adopt = new Adopt();
                Adopt.Address = adopt.Address;
                Adopt.Name = adopt.Name;
                Adopt.Neutered = adopt.Neutered;
                Adopt.Age = adopt.Age;
                Adopt.Breed = adopt.Breed;
                Adopt.Vaccinated = adopt.Vaccinated;
                Adopt.GenderId = adopt.GenderId;
                Adopt.PetSizeId = adopt.PetSizeId;
                Adopt.AgeTypeId = adopt.AgeTypeId;

                Adopt.CreatedDate = DateTime.Now;

                db.Adopts.Add(Adopt);
                db.SaveChanges();

                foreach (HttpPostedFileBase image in adopt.ImageFile)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + image.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                    image.SaveAs(imagePath);//hə hə

                    SlideAdopt slideAdopt = new SlideAdopt();
                    slideAdopt.Image = imageName;
                    slideAdopt.AdoptId = Adopt.Id;

                    db.SlideAdopts.Add(slideAdopt);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.Gender = db.Genders.ToList();
            ViewBag.PetSize = db.PetSizes.ToList();
            ViewBag.AgeType = db.AgeTypes.ToList();

            return View(adopt);
        }

        // GET: Admin/Adopt/Update

        public ActionResult Update(int id)
        {
            Adopt adopt = db.Adopts.Find(id);
            if (adopt == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender = db.Genders.ToList();
            ViewBag.PetSize = db.PetSizes.ToList();
            ViewBag.AgeType = db.AgeTypes.ToList();

            return View(adopt);
        }

        // POST: Admin/Adopt/Update

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Adopt adopt)
        {
            if (ModelState.IsValid) 
            { 
                Adopt Adopt = db.Adopts.Include("SlideAdopts").FirstOrDefault(p => p.Id ==adopt.Id);
                if (adopt.ImageFile.Length > 1)
                {
                    foreach (SlideAdopt item in Adopt.SlideAdopts.ToList())
                    {
                        string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), item.Image);
                        System.IO.File.Delete(oldImagePath);

                        db.SlideAdopts.Remove(item);
                        db.SaveChanges();
                    }

                    foreach (HttpPostedFileBase image in adopt.ImageFile)
                    {
                        string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + image.FileName;
                        string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                        image.SaveAs(imagePath);
                        SlideAdopt slideAdopt = new SlideAdopt();
                        slideAdopt.Image = imageName;
                        slideAdopt.AdoptId = adopt.Id;

                        db.SlideAdopts.Add(slideAdopt);
                        db.SaveChanges();
                    }
                }
                Adopt.Address = adopt.Address;
                Adopt.Name = adopt.Name;
                Adopt.Neutered = adopt.Neutered;
                Adopt.Age = adopt.Age;
                Adopt.Breed = adopt.Breed;
                Adopt.Vaccinated = adopt.Vaccinated;
                Adopt.GenderId = adopt.GenderId;
                Adopt.PetSizeId = adopt.PetSizeId;
                Adopt.AgeTypeId = adopt.AgeTypeId;

                db.Entry(Adopt).State = EntityState.Modified;
                db.Entry(Adopt).Property(c => c.CreatedDate).IsModified = false;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Gender = db.Genders.ToList();
            ViewBag.PetSize = db.PetSizes.ToList();
            ViewBag.AgeType = db.AgeTypes.ToList();

            return View(adopt);
        }

        // GET: Admin/Adopt/Delete

        public ActionResult Delete(int id)
        {
            Adopt adopt = db.Adopts
                          .Include("Gender")
                          .Include("PetSize")
                          .Include("AgeType")
                          .Include("SlideAdopts")
                          .FirstOrDefault(p => p.Id == id);
            if (adopt == null)
            {
                return HttpNotFound();
            }
            foreach (var item in db.SlideAdopts.ToList())
            {
                if (item.AdoptId == id)
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads"), item.Image);

                    System.IO.File.Delete(imagePath);
                    db.SlideAdopts.Remove(item);
                }
            }
            foreach (var item1 in db.Appointments.ToList())
            {
                if (item1.AdoptId == id)
                {
                    db.Appointments.Remove(item1);
                }
            }
            db.Adopts.Remove(adopt);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Adopt/AdoptRule

        [ValidateInput(false)]
        public ActionResult AdoptRule()
        {
            List<AdoptRule> adoptRules = db.AdoptRules.ToList();

            return View(adoptRules);
        }

        // GET: Admin/Adopt/AdoptRule/Update

        [ValidateInput(false)]
        public ActionResult RuleUpdate(int Id)
        {
            AdoptRule adoptRule = db.AdoptRules.Find(Id);

            if (adoptRule == null)
            {
                return HttpNotFound();
            }
            return View(adoptRule);
        }

        // POST: Admin/Adopt/AdoptRule/Update

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RuleUpdate(AdoptRule adoptRule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adoptRule).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("AdoptRule");
            }
            return View(adoptRule);
        }
    }
}