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
    public class AppointmentController : Controller
    {
        // GET: Admin/Appointment
        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            List<Appointment> adopts = db.Appointments
                                       .Include("Service")
                                       .Include("Adopt").
                                       Where(w => (!string.IsNullOrEmpty(searchText) ? w.Name.Contains(searchText) : true)
                                       || (!string.IsNullOrEmpty(searchText) ? w.Phone.Contains(searchText) : true)
                                       || (!string.IsNullOrEmpty(searchText) ? w.Email.Contains(searchText) : true)
                                       || (!string.IsNullOrEmpty(searchText) ? w.Message.Contains(searchText) : true)).ToList();
            return View(adopts);
        }

        // GET: Admin/Appointment/Delete

        public ActionResult Delete(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}