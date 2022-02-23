using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using PostMidProject.Models;

namespace PostMidProject.Controllers
{
    public class Message
    {
        public static string message { set; get; } = "";
    }

    public class HomeController : Controller
    {
        Database1Entities4 db = new Database1Entities4();
        // GET: Home
        public ActionResult index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult about()
        {
            return View();
        }
        [HttpGet]
        public ActionResult contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult contact(string Name, string EmailId, string PhoneNo, string Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("valav64683@pidhoes.com"));
                mail.From = new MailAddress("valav64683@pidhoes.com");
                mail.Subject = Name;

                string userMessage = "";
                userMessage = "<br/>Name :" + Name;
                userMessage = userMessage + "<br/>Email Id: " + EmailId;
                userMessage = userMessage + "<br/>Phone No: " + PhoneNo;


                userMessage = userMessage + "<br/>Message: " + Message;

                string Body = "Hi, <br/><br/> A new enquiry by user. Detail is as follows:<br/><br/> " + userMessage + "<br/><br/>Thanks";


                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                //SMTP Server Address of gmail
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("valav64683@pidhoes.com", ""); // your email id and password
                // Smtp Email ID and Password For authentication
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ViewBag.Message = "Thank you for contacting us.";
            }
            catch
            {
                ViewBag.Message = "Error............";
            }

            return View();
        }
        public ActionResult shop()
        {
            return View();
        }

        public ActionResult glasses()
        {
            return View(db.Products.ToList());
        }

        [HttpPost]
        public ActionResult loginpost()
        {
            string email = Request["email"];
            string password = Request["password"];
            user a = db.users.Where(s => s.Email == email && s.Password == password).FirstOrDefault();
            if (a == null || a.Password != password)
            {
                Message.message = "Invalid User Name or Password";
                return RedirectToAction("LoginForm");
            }
            else
            {
                Session["username"] = email;
                Session["id"] = a.User_Id;
                if (a.Role_id == 2)
                {
                    return RedirectToAction("buyGlasses", "Customer");
                    //return RedirectToAction("/Customer/buyGlasses");
                }
                return RedirectToAction("AdminDashboard", "Admin");
                //return RedirectToAction("/Admin/AdminDashboard");

            }
        }

        [HttpPost]
        public ActionResult signuppost()
        {
            string email = Request["email"];
            string name = Request["name"];
            string phone = Request["phone"];
            string password = Request["password"];
            user a = db.users.Where(s => s.Name == email).FirstOrDefault();
            if (a == null)
            {
                a = new user();
                a.Name = name;
                a.Email = email;
                a.Phone = phone;
                a.Password = password;
                a.Role_id = 2;
                db.users.Add(a);
                db.SaveChanges();

            }
            else
            {
                Message.message = "User Name Already Exist";
                return RedirectToAction("LoginForm");
            }
            return Redirect("LoginForm");
        }



        public ActionResult LoginForm()
        {
            return View();
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("LoginForm");

        }
    }
}