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
    public class TeamController : Controller
    {
        // GET: Admin/Team

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            return View(db.Teams.Where(w => (!string.IsNullOrEmpty(searchText) ? w.Fullname.Contains(searchText) : true)
                        || (!string.IsNullOrEmpty(searchText) ? w.Position.Name.Contains(searchText) : true)).ToList());
        }

        // GET: Admin/Team/Details

        public ActionResult Details(int Id)
        {
            Team team = db.Teams.Find(Id);

            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Admin/Team/Create
        public ActionResult Create()
        {
            ViewBag.Positions = db.Positions.ToList();

            return View();
        }

        // POST: Admin/Team/Create

        [HttpPost]
        public ActionResult Create(Team team)
        {
            if (ModelState.IsValid)
            {
                Team Team = new Team();

                if (team.ImageFile == null)
                {
                    ModelState.AddModelError("", "Image is required");

                    ViewBag.Positions = db.Positions.ToList();

                    return View(team);
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + team.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    team.ImageFile.SaveAs(imagePath);
                    Team.Image = imageName;
                }
                Team.Fullname = team.Fullname;
                Team.PositionId = team.PositionId;

                db.Teams.Add(Team);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Positions = db.Positions.ToList();

            return View(team);
        }

        // GET: Admin/Team/Update

        public ActionResult Update(int id)
        {
            Team team = db.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.Positions = db.Positions.ToList();

            return View(team);
        }

        // POST: Admin/Team/Update

        [HttpPost]
        public ActionResult Update(Team team)
        {
            if (ModelState.IsValid)
            {
                Team Team = db.Teams.Find(team.Id);

                if (team.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + team.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string OldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Team.Image);

                    team.ImageFile.SaveAs(imagePath);
                    System.IO.File.Delete(OldImagePath);

                    Team.Image = imageName;
                }
                Team.Fullname = team.Fullname;
                Team.PositionId = team.PositionId;
                
                db.Entry(Team).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.Positions = db.Positions.ToList();

            return View(team);
        }

        // GET: Admin/Team/Delete

        public ActionResult Delete(int id)
        {
            Team team = db.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }
            db.Teams.Remove(team);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}