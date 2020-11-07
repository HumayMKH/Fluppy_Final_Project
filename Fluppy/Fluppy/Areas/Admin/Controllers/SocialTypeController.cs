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
    public class SocialTypeController : Controller
    {
        // GET: Admin/SocialType

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<SocialType> socialTypes = db.SocialTypes.ToList();

            return View(socialTypes);
        }

        // GET: Admin/SocialType/Create

        public ActionResult Create()
        {
                return View();
        }

        // POST: Admin/SocialType/Create

        [HttpPost]
        public ActionResult Create(SocialType socialType)
        {
            if (ModelState.IsValid)
            {
                db.SocialTypes.Add(socialType);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(socialType);
        }

        // GET: Admin/SocialType/Update

        public ActionResult Update(int id)
        {
            SocialType socialType = db.SocialTypes.Find(id);

            if (socialType == null)
            {
                return HttpNotFound();
            }
            return View(socialType);
            }

        // POST: Admin/SocialType/Update

        [HttpPost]
        public ActionResult Update(SocialType socialType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialType).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(socialType);
        }

        // GET: Admin/SocialType/Delete

        public ActionResult Delete(int id)
        {
            SocialType socialType = db.SocialTypes.Find(id);

            if (socialType == null)
            {
                return HttpNotFound();
            }
            db.SocialTypes.Remove(socialType);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
