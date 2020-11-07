using Fluppy.Areas.Admin.Filters;
using Fluppy.DAL;
using Fluppy.Models;
using Fluppy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Fluppy.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/HomeSettings/Dashboard

        FluppyContext db = new FluppyContext();

        [logout]
        public ActionResult Dashboard()
        {
            ViewBag.Adopt = db.Appointments.Where(a => a.AdoptId != null).Count();
            ViewBag.Service = db.Appointments.Where(a => a.ServiceId != null).Count();
            ViewBag.Sale = db.Sales.Count();
            ViewBag.Contact = db.Contacts.Count();

            return View();
        }

        // GET: Admin/HomeSettings
        [logout]
        public ActionResult Index()
        {
            List<HomeSetting> homeSettings = db.HomeSettings.ToList();

            return View(homeSettings);
        }

        // GET: Admin/HomeSettings/Details
        [logout]
        public ActionResult Details(int Id)
        {
            HomeSetting homeSetting = db.HomeSettings.Find(Id);

            if (homeSetting == null)
            {
                return HttpNotFound();
            }
            return View(homeSetting);
        }

        // GET: Admin/HomeSettings/Update
        [logout]
        public ActionResult Update(int id)
        {
            HomeSetting homeSetting = db.HomeSettings.Find(id);

            if (homeSetting == null)
            {
                return HttpNotFound();
            }
            return View(homeSetting);
        }

        // POST: Admin/HomeSettings/Update

        [logout]
        [HttpPost]
        public ActionResult Update(HomeSetting homeSetting)
        {
            if (ModelState.IsValid)
            {
                HomeSetting HomeSettings = db.HomeSettings.Find(homeSetting.Id);
            
                if (homeSetting.LogoFile != null)
                {
                    string logoName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + homeSetting.LogoFile.FileName;
                    string logoPath = Path.Combine(Server.MapPath("~/Uploads/"), logoName);
                
                    string OldLogoPath = Path.Combine(Server.MapPath("~/Uploads/"), HomeSettings.Logo);
                    System.IO.File.Delete(OldLogoPath);
                
                    homeSetting.LogoFile.SaveAs(logoPath);
                    HomeSettings.Logo = logoName;
                }
            
                if (homeSetting.FooterLogoFile != null)
                {
                    string footerlogoName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + homeSetting.FooterLogoFile.FileName;
                    string footerlogoPath = Path.Combine(Server.MapPath("~/Uploads/"), footerlogoName);
                
                    string OldfooterLogoPath = Path.Combine(Server.MapPath("~/Uploads/"), HomeSettings.FooterLogo);
                    System.IO.File.Delete(OldfooterLogoPath);
                
                    homeSetting.FooterLogoFile.SaveAs(footerlogoPath);
                    HomeSettings.FooterLogo = footerlogoName;
                }
                HomeSettings.Address = homeSetting.Address;
                HomeSettings.CopyRight = homeSetting.CopyRight;
                HomeSettings.FooterContent = homeSetting.FooterContent;
                HomeSettings.Email = homeSetting.Email;
                HomeSettings.Name = homeSetting.Name;
                HomeSettings.Phone = homeSetting.Phone;
                HomeSettings.StartTime= homeSetting.StartTime;
                HomeSettings.EndTime = homeSetting.EndTime;
                HomeSettings.SatStartTime = homeSetting.SatStartTime;
                HomeSettings.SatEndTime = homeSetting.SatEndTime;
            
                db.Entry(HomeSettings).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(homeSetting);
        }

        // GET: Admin/HomeSettings/Delete
        [logout]
        public ActionResult Delete(int id)
        {
             HomeSetting homeSetting = db.HomeSettings.Find(id);

             if (homeSetting == null)
             {
                 return HttpNotFound();
                
             }                
             db.HomeSettings.Remove(homeSetting);
             db.SaveChanges();

             return RedirectToAction("Index");
        }

        // GET: Admin/HomeSettings/Subscribe
        [logout]
        public ActionResult Subscribe()
        {
            List<Subscribe> subscribes = db.Subscribes.ToList();

            return View(subscribes);
        }

        // GET: Admin/HomeSettings/Login

        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/HomeSettings/Login

        [HttpPost]
        public ActionResult Login(VmLogin login)
        {
            if (ModelState.IsValid)
            {
                Models.Admins admin = db.Admins.FirstOrDefault(a => a.Username == login.Username);

                if (admin != null)
                {
                    if (Crypto.VerifyHashedPassword(admin.Password, login.Password) == true)
                    {
                        Session["Loginner"] = admin;
                        Session["LoginnerId"] = admin.Id;

                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Wronge");
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "Wronge");
                }
            }
            return View();
        }

        // POST: Admin/HomeSettings/Profil
        [logout]
        public ActionResult Profil()
        {
            Admins admin = (Admins)Session["Loginner"];
            return View(admin);
        }

        // POST: Admin/HomeSettings/Logout

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Login"); 
        }
    }
}