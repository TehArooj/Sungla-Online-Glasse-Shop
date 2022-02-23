using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostMidProject.Models;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;

namespace PostMidProject.Controllers
{
    public class CustomerController : Controller
    {
        Database1Entities4 db = new Database1Entities4();

        // GET: Customer

        public ActionResult myOrders()
        {

            int userId = Int32.Parse(Session["id"].ToString());
            return View(db.Orders.Where(x => x.User_Id == userId).ToList());
        }

        public ActionResult buyGlasses()
        {
            return View(db.Products.ToList());
        }

        public ActionResult AddOrder(int id)
        {
            int userId = Int32.Parse(Session["id"].ToString());
            Product p = db.Products.Where(x => x.Product_Id == id).FirstOrDefault();
            Order o = new Order();
            o.User_Id = userId;
            o.Date = DateTime.Now;
            o.Status = 1;
            o.Total_Amount = p.Price;
            o.Product_Id = p.Product_Id;
            db.Orders.Add(o);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                // Save failed.
                return RedirectToAction("buyGlasses", "Customer", new { orderPlaced = "0" });
            }

            // Save successful
            return RedirectToAction("buyGlasses", "Customer", new { orderPlaced = "1" });
        }

        public ActionResult CancelOrder(int id)
        {
            Order o = db.Orders.Find(id);
            o.Status = 3;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                // Save failed.
                return RedirectToAction("myOrders", "Customer", new { canceled = "0" });
            }

            // Save successful
            return RedirectToAction("myOrders", "Customer", new { canceled = "1" });
        }

    }
}