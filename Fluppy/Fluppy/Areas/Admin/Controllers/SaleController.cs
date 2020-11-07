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
    public class SaleController : Controller
    {
        // GET: Admin/Sale

        FluppyContext db = new FluppyContext();

        public ActionResult Index(string searchText)
        {
            List<Sale> sales = db.Sales
                               .Include("Product")
                               .Include("User").
                               Where(w => (!string.IsNullOrEmpty(searchText) ? w.Price.ToString().Contains(searchText) : true)
                               || (!string.IsNullOrEmpty(searchText) ? w.User.Name.Contains(searchText) : true)
                               || (!string.IsNullOrEmpty(searchText) ? w.Product.Name.Contains(searchText) : true)).ToList();
            return View(sales);
        }

        // GET: Admin/Sale/Details

        public ActionResult Details(int Id)
        {
            Sale sale = db.Sales.Find(Id);

            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Admin/Sale/Delete

        public ActionResult Delete(int id)
        {
            Sale sale = db.Sales.Find(id);

            if (sale == null)
            {
                return HttpNotFound();
            }
            db.Sales.Remove(sale);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}