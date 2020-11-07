using Fluppy.Areas.Admin.Filters;
using Fluppy.DAL;
using Fluppy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Areas.Admin.Controllers
{
    [logout]
    public class ContactController : Controller
    {
        // GET: Admin/Contact

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            List<Contact> contacts = db.Contacts.Where(w => (!string.IsNullOrEmpty(searchText) ? w.Fullname.Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.Phone.Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.Message.Contains(searchText) : true)
                                     || (!string.IsNullOrEmpty(searchText) ? w.Email.Contains(searchText) : true)).ToList();
            return View(contacts);
        }

        // GET: Admin/Contact/Message Delete

        public ActionResult Delete(int id)
        {
            Contact contact = db.Contacts.Find(id);

            if (contact == null)
            {
                return HttpNotFound();
            }
            db.Contacts.Remove(contact);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}