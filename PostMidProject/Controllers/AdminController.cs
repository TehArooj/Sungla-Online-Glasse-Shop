using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostMidProject.Models;
using System.IO;

namespace PostMidProject.Controllers
{
    //public class Message
    //{
    //    public static string message { set; get; } = "";
    //}
    public class AdminController : Controller
    {
        Database1Entities4 db = new Database1Entities4();
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult AllProducts()
        {
            return View(db.Products.ToList());
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(ProductFileUploadViewModels data)
        {

            string name = data.product.Product_Name;
            int price = (int)data.product.Price;
            //int quantity = (int) data.product.Quantity;
            string image = (Path.GetFileName(data.ImageFile.FileName));

            Product p = new Product();
            p.Product_Name = name;
            p.Price = (price);
            //p.Quantity = (quantity);
            p.Image = image;
            System.Diagnostics.Debug.WriteLine(Session["username"]);
            System.Diagnostics.Debug.WriteLine("username");
            if (Session["username"] == null || Session["username"].ToString() == "")
            {
                return RedirectToAction("LoginForm", "Home");
            }
            string username = Session["username"].ToString();
            user u = db.users.Where(s => s.Name == username).FirstOrDefault();
            db.Products.Add(p);

            try
            {

                //Method 2 Get file details from HttpPostedFileBase class    

                if (data.ImageFile != null)
                {
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(data.ImageFile.FileName));
                    data.ImageFile.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading."; ;
            }

            try
            {
                db.SaveChanges();
            }
            catch
            {
                // Save failed.
                return RedirectToAction("AddProduct", "Admin", new { added = "0" });
            }

            // Save successful
            return RedirectToAction("AddProduct", "Admin", new { added = "1" });
        }

        public ActionResult Orders()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult UpdateProduct(int id)
        {
            Product p = db.Products.Find(id);
            return View(p);
        }



        [HttpPost]
        public ActionResult UpdateProductConfirm(int id)
        {
            Product p = db.Products.Find(id);
            string name = Request["name"];
            string price = Request["price"];
            //string quantity = Request["qty"];
            //string image = Request["image"];
            p.Product_Name = name;
            p.Price = Int32.Parse(price);
            //p.Quantity = Int32.Parse(quantity);
            //p.Image = image;

            try
            {
                db.SaveChanges();
            }
            catch
            {
                // Save failed.
                return RedirectToAction("AllProducts", "Admin", new { updated = "0" });
            }

            // Save successful
            return RedirectToAction("AllProducts", "Admin", new { updated = "1" });
        }

        public ActionResult DeleteProduct(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                // Save failed.
                return RedirectToAction("AllProducts", "Admin", new { deleted = "0" });
            }

            // Save successful
            return RedirectToAction("AllProducts", "Admin", new { deleted = "1" });
        }


        public ActionResult DeliverOrder(int id)
        {
            Order o = db.Orders.Find(id);
            o.Status = 2;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                // Save failed.
                return RedirectToAction("Orders", "Admin", new { delivered = "0" });
            }

            // Save successful
            return RedirectToAction("Orders", "Admin", new { delivered = "1" });
        }

        public ActionResult AllCustomers()
        {
            return View(db.users.Where(x => x.Role_id == 2).ToList());

        }
    }
}