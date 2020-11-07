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
    public class HomeSocialController : Controller
    {
        // GET: Admin/HomeSocial

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
                List<HomeSocial> homeSocials = db.HomeSocials.Include("HomeSetting").Include("SocialType").ToList();

                return View(homeSocials);
        }

        // GET: Admin/HomeSocial/Create
        public ActionResult Create()
        {
            ViewBag.Home = db.HomeSettings.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View();
        }

        // POST: Admin/HomeSocial/Create

        [HttpPost]
        public ActionResult Create(HomeSocial homeSocial)
        {
            if (ModelState.IsValid)
            {
                db.HomeSocials.Add(homeSocial);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Home = db.HomeSettings.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View(homeSocial);
        }

        // GET: Admin/HomeSocial/Update

        public ActionResult Update(int id)
        {
            HomeSocial homeSocial = db.HomeSocials.Find(id);

            if (homeSocial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Home = db.HomeSettings.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View(homeSocial);
        }

        // POST: Admin/HomeSocial/Update

        [HttpPost]
        public ActionResult Update(HomeSocial homeSocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeSocial).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Home = db.HomeSettings.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View(homeSocial);
        }

        // GET: Admin/HomeSocial/Delete
        public ActionResult Delete(int id)
        {
            HomeSocial homeSocial = db.HomeSocials.Find(id);

            if (homeSocial == null)
            {
                return HttpNotFound();
            }
            db.HomeSocials.Remove(homeSocial);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}