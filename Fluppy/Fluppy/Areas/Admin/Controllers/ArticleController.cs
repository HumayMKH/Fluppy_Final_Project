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
    public class ArticleController : Controller
    {
        // GET: Admin/Article

        FluppyContext db = new FluppyContext();

        public ActionResult Index()
        {
            List<Article> article = db.Articles.ToList();

            return View(article);
        }

        // GET: Admin/Article/Details

        public ActionResult Details(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Admin/Article/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Article/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                if (article.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + article.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    article.ImageFile.SaveAs(imagePath);
                    article.Image = imageName;
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Image is required");
                    return View(article);
                }
                article.CreatedDate = DateTime.Now;
                article.AdminId =(int) Session["LoginnerId"];
                db.Articles.Add(article);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Admin/Article/Update

        public ActionResult Update(int id)
        {
            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Admin/Article/Update

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Article article)
        {
            if (ModelState.IsValid)
            {
                Article Article = db.Articles.Find(article.Id);
                if (article.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + article.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Article.Image);
                    System.IO.File.Delete(oldImagePath);

                    article.ImageFile.SaveAs(imagePath);
                    Article.Image = imageName;
                }
                Article.Title = article.Title;
                Article.Date = article.Date;
                Article.Content = article.Content;

                db.Entry(Article).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Admin/Article/Delete

        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            db.Articles.Remove(article);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}