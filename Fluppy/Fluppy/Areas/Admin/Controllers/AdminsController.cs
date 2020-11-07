using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Fluppy.Areas.Admin.Filters;
using Fluppy.DAL;
using Fluppy.Models;

namespace Fluppy.Areas.Admin.Controllers
{ 
    [logout]
    public class AdminsController : Controller
    {
        private FluppyContext db = new FluppyContext();

        // GET: Admin/Admins

        public ActionResult Index(string searchText)
        {
            return View(db.Admins.Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                                 || (!string.IsNullOrEmpty(searchText) ? w.Surname.Contains(searchText) : true)
                                 || (!string.IsNullOrEmpty(searchText) ? w.Username.Contains(searchText) : true)
                                 || (!string.IsNullOrEmpty(searchText) ? w.Email.Contains(searchText) : true)).ToList());
        }

        // GET: Admin/Admins/Details

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admins admins = db.Admins.Find(id);

            if (admins == null)
            {
                return HttpNotFound();
            }
            return View(admins);
        }

        // GET: Admin/Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Admins/Create

        [HttpPost]
        public ActionResult Create(Admins admins)
        {
            if (ModelState.IsValid)
            {
                if (admins.Password == null)
                {
                    ModelState.AddModelError("Password", "Admin password not find!");
                    return View(admins);
                }
                if (admins.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + admins.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    admins.ImageFile.SaveAs(imagePath);
                    admins.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View(admins);
                }
                admins.CreatedDate = DateTime.Now;
                admins.Password = Crypto.HashPassword(admins.Password);
                admins.MainAdmin = false;
               

                db.Admins.Add(admins);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(admins);
        }

        // GET: Admin/Admins/Update

        public ActionResult Update(int id)
        {
            Admins admins = db.Admins.Find(id);
            if (admins == null)
            {
                return HttpNotFound();
            }
            return View(admins);
        }

        // POST: Admin/Admins/Update
      
        [HttpPost]
        public ActionResult Update(Admins admins)
        {
            if (ModelState.IsValid)
            {
                Admins Admins = db.Admins.Find(admins.Id);
                if (admins.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + admins.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Admins.Image);
                    System.IO.File.Delete(oldImagePath);

                    admins.ImageFile.SaveAs(imagePath);
                    Admins.Image = imageName;
                }
                Admins.Email = admins.Email;
                Admins.Name = admins.Name;
                Admins.Surname = admins.Surname;
                Admins.Username = admins.Username;


                db.Entry(Admins).State = EntityState.Modified;
                db.Entry(Admins).Property(c => c.CreatedDate).IsModified = false;
                db.Entry(Admins).Property(c => c.MainAdmin).IsModified = false;
                db.Entry(Admins).Property(c => c.Password).IsModified = false;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admins);
        }

        // GET: Admin/Admins/Delete

        public ActionResult Delete(int? Id)
        {
            Admins admins = db.Admins.Find(Id);
            try
            {
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (admins == null)
                {
                    return HttpNotFound();
                }
                db.Admins.Remove(admins);
                db.SaveChanges();

            }
            catch(System.IndexOutOfRangeException e)
            {
               throw new System.ArgumentOutOfRangeException("Admin used other tables!", e);
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Admins/Reset

        public ActionResult Reset(int Id)
        {
            Admins admins = db.Admins.Find(Id);

            if (admins == null)
            {
                return HttpNotFound();
            }
            admins.Password = Crypto.HashPassword("12345");

            db.Entry(admins).State = EntityState.Modified;
            db.Entry(admins).Property(c => c.CreatedDate).IsModified = false;
            db.Entry(admins).Property(c => c.MainAdmin).IsModified = false;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
