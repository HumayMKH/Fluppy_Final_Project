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
    public class PositionController : Controller
    {
        // GET: Admin/Position

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<Position> positions = db.Positions.ToList();

            return View(positions);
        }

        // GET: Admin/Position/Details

        public ActionResult Details(int id)
        {
            Position position = db.Positions.Find(id);

            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: Admin/Position/Create

        public ActionResult Create()
        {
                return View();
        }

        // POST: Admin/Position/Create

        [HttpPost]
        public ActionResult Create(Position position)
        {
             if (ModelState.IsValid)
             {
                 db.Positions.Add(position);
                 db.SaveChanges();

                 return RedirectToAction("Index");
             }
             return View(position);
        }

        // GET: Admin/Position/Update

        public ActionResult Update(int id)
        {
            Position position = db.Positions.Find(id);

            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Admin/Position/Update

        [HttpPost]
        public ActionResult Update(Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: Admin/Position/Delete

        public ActionResult Delete(int id)
        {
            Position position = db.Positions.Find(id);

            if (position == null)
            {
                return HttpNotFound();
            }
            db.Positions.Remove(position);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}