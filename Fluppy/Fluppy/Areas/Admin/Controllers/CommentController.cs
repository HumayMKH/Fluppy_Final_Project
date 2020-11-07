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
    public class CommentController : Controller
    {
        // GET: Admin/Comment

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            List<Comment> comments = db.Comments.Where(w => (!string.IsNullOrEmpty(searchText) ? w.Content.Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.User.Name.Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.Blog.Title.Contains(searchText) : true)).ToList();
            return View(comments);
        }

        // GET: Admin/Comment/Details

        public ActionResult Details(int id)
        {
            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comment/Update

        public ActionResult Update(int id)
        {
            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comment/Update

        [HttpPost]
        public ActionResult Update(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.Entry(comment).Property(c => c.CreatedDate).IsModified = false;
                db.Entry(comment).Property(c => c.BlogId).IsModified = false;
                db.Entry(comment).Property(c => c.UserId).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Admin/Comment/Delete

        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}