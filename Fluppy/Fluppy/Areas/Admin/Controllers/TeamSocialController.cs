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
    public class TeamSocialController : Controller
    {
        // GET: Admin/TeamSocial

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<TeamSocial> teamSocials = db.TeamSocials.Include("Team").Include("SocialType").ToList();

            return View(teamSocials);
        }

        // GET: Admin/TeamSocial/Create
        public ActionResult Create()
        {
            ViewBag.Team = db.Teams.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View();
        }

        // POST: Admin/TeamSocial/Create

        [HttpPost]
        public ActionResult Create(TeamSocial teamSocial)
        {
            if (ModelState.IsValid)
            {
                db.TeamSocials.Add(teamSocial);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Team = db.Teams.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View(teamSocial);
        }

        // GET: Admin/TeamSocial/Update
        public ActionResult Update(int id)
        {
            TeamSocial teamSocial = db.TeamSocials.Find(id);

            if (teamSocial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = db.Teams.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View(teamSocial);
        }

        // POST: Admin/TeamSocial/Update

        [HttpPost]
        public ActionResult Update(TeamSocial teamSocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamSocial).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Team = db.Teams.ToList();
            ViewBag.SocialType = db.SocialTypes.ToList();

            return View(teamSocial);
        }

        // GET: Admin/TeamSocial/Delete

        public ActionResult Delete(int id)
        {
            TeamSocial teamSocial = db.TeamSocials.Find(id);

            if (teamSocial == null)
            {
                return HttpNotFound();
            }
            db.TeamSocials.Remove(teamSocial);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}