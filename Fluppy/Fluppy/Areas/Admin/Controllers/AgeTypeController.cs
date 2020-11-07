using Fluppy.Areas.Admin.Filters;
using Fluppy.DAL;
using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Areas.Admin.Controllers
{
    [logout]
    public class AgeTypeController : Controller
    {
        // GET: Admin/AgeType

        FluppyContext db = new FluppyContext();
        public ActionResult Index()
        {
            List<AgeType> agetype = db.AgeTypes.ToList();

            return View(agetype);
        }

        // GET: Admin/AgeType/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AgeType/Create

        [HttpPost]
        public ActionResult Create(AgeType agetype)
        {
            if (ModelState.IsValid)
            {
                db.AgeTypes.Add(agetype);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(agetype);
        }

        // GET: Admin/AgeType/Update

        public ActionResult Update(int id)
        {
            AgeType agetype = db.AgeTypes.Find(id);
            if (agetype == null)
            {
                return HttpNotFound();
            }
            return View(agetype);
        }

        // POST: Admin/AgeType/Update

        [HttpPost]
        public ActionResult Update(AgeType agetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agetype).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agetype);
        }

        // GET: Admin/AgeType/Delete
        public ActionResult Delete(int id)
        {
            AgeType agetype = db.AgeTypes.Find(id);
            if (agetype == null)
            {
                return HttpNotFound();
            }
            db.AgeTypes.Remove(agetype);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}