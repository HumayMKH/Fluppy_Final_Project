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
    public class BlogController : Controller
    {
        // GET: Admin/Blog

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            List<Blog> blogs = db.Blogs
                               .Include("BlogCategory")
                               .Include("Admin").
                               Where(w => (!string.IsNullOrEmpty(searchText) ? w.Title.Contains(searchText) : true)
                               || (!string.IsNullOrEmpty(searchText) ? w.Content.Contains(searchText) : true)).ToList();
            return View(blogs);
        }

        // GET: Admin/Blog/Details

        public ActionResult Details(int Id)
        {
            Blog blog = db.Blogs.Find(Id);

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Admin/Blog/Create
        public ActionResult Create()
        {
            ViewBag.BlogCategory = db.BlogCategories.ToList();

            return View();
        }

        // POST: Admin/Blog/Create

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                    Blog Blog = new Blog();
                if (blog.ImageFile == null)
                {
                    ModelState.AddModelError("", "Image is required");
                    ViewBag.BlogCategory = db.BlogCategories.ToList();

                    return View(blog);
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + blog.ImageFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);
                
                    blog.ImageFile.SaveAs(imagePath);
                    Blog.Image = imageName;
                
                }
                if (blog.ImageFileBig == null)
                {
                    ModelState.AddModelError("", "Image is required");
                    ViewBag.BlogCategory = db.BlogCategories.ToList();

                    return View(blog);
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + blog.ImageFileBig.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    blog.ImageFileBig.SaveAs(imagePath);
                    Blog.ImageBig = imageName;

                }
                Blog.ViewCount = blog.ViewCount;
                Blog.Title = blog.Title;
                Blog.BlogCategoryId = blog.BlogCategoryId;
                Blog.Content = blog.Content;
                Blog.CreatedDate = DateTime.Now;
                Blog.AdminId = (int)Session["LoginnerId"];
                Blog.Date = blog.Date;
                
                db.Blogs.Add(Blog);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.BlogCategory = db.BlogCategories.ToList();

            return View(blog);
        }

        // GET: Admin/Blog/Update

        public ActionResult Update(int Id)
        {
            Blog blog = db.Blogs.Find(Id);

            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogCategory = db.BlogCategories.ToList();

            return View(blog);
        }

        // POST: Admin/Blog/Update

        [HttpPost]
        public ActionResult Update(Blog blog)
        {
            if (ModelState.IsValid)
            {
                Blog Blog = db.Blogs.Find(blog.Id);

            if (blog.ImageFile != null)
            {
                string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + blog.ImageFile.FileName;
                string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Blog.Image);
                System.IO.File.Delete(oldImagePath);

                blog.ImageFile.SaveAs(imagePath);
                Blog.Image = imageName;
            }

            if (blog.ImageFileBig != null)
            {
                string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + blog.ImageFileBig.FileName;
                string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), Blog.ImageBig);
                System.IO.File.Delete(oldImagePath);

                blog.ImageFileBig.SaveAs(imagePath);
                Blog.ImageBig = imageName;
            }
            Blog.ViewCount = blog.ViewCount;
            Blog.Title = blog.Title;
            Blog.BlogCategoryId = blog.BlogCategoryId;
            Blog.Content = blog.Content;
            Blog.CreatedDate = DateTime.Now;

            db.Entry(Blog).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
            }
            ViewBag.BlogCategory = db.BlogCategories.ToList();

            return View(blog);
            }

        // GET: Admin/Blog/Delete

        public ActionResult Delete(int id)
        {
            Blog blog = db.Blogs.Find(id);

            if (blog == null)
            {
                return HttpNotFound();
            }
            db.Blogs.Remove(blog);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
