using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class UsersController : Controller
    {
        FluppyContext db = new FluppyContext();

        // GET: Admin/Users

        public ActionResult Index(string searchText)
        {
            return View(db.Users.Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Surname.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Username.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Email.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.PostCode.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.City.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Address.Contains(searchText) : true)).ToList());
        }

        // GET: Admin/Users/Details

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create

        [HttpPost]
        public ActionResult Create(User users)
        {
            if (ModelState.IsValid)
            {
                users.Password = Crypto.HashPassword(users.Password);
                users.CreatedDate = DateTime.Now;
                users.IsRegistered = true;

                db.Users.Add(users);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Admin/Users/Update
        public ActionResult Update(int id)
        {
            User users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Admin/Users/Update

        [HttpPost]
        public ActionResult Update(User users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.Entry(users).Property(c => c.CreatedDate).IsModified = false;
                db.Entry(users).Property(c => c.IsRegistered).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Admin/Users/Delete
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
