using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EventWeb.Security;
using Model;
using Service;

namespace EventWeb.Controllers
{

    

    public class AdminController : Controller
    {
        IserviceAdmin spa = new serviceAdmin();
        //getting the instance of service that way i can use the service pattern and admin service
        


        public ActionResult login()
        {
            Session["AdminID"] = null;//setting the session to be null
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad,string ReturnUrl)
        {

            if(spa.authAdmin(ad.mailAdmin,ad.passwordAdmin))//check serviceAdmin
            {
                FormsAuthentication.SetAuthCookie(ad.mailAdmin, false);//store user mail in cookies 
                Admin _admin = new Admin();
                _admin = spa.Get(x => x.mailAdmin == ad.mailAdmin && x.passwordAdmin == ad.passwordAdmin);
                Session["AdminID"] = _admin.idAdmin;
                Session["mailAdmin"] = _admin.mailAdmin;


                //Admin _admin = (spa.Get(x => x.mailAdmin == ad.mailAdmin));
                //HttpCookie mycookie = new HttpCookie("Role");
                //
                //if (_admin.isSuperAdmin)
                //{
                //    Session["Role"] = "SuperAdmin";
                //    HttpContext.Session["Role"] = Session["Role"];
                //    mycookie.Values["Role"] = Session["Role"].ToString();
                //}
                //else
                //{
                //    Session["Role"] = "Admin";
                //    HttpContext.Session["Role"] = Session["Role"];
                //    mycookie.Values["Role"] = Session["Role"].ToString();
                //}

                return RedirectToAction("index");
            }



            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index");
        }

        [CustomAuthorizeAttribute(Roles="SuperAdmin")]
        public ActionResult ListAdmin()
        {//only super admin can view list admins and manage admins(edit,delete)

            List<Admin> _admin = new List<Admin>();
            _admin = spa.GetMany(x => x.isSuperAdmin != true).ToList();//get you all lines in the table admin without the super admin
            ViewData.Model = _admin;

            return View();
        }

        // GET: Admin
        public ActionResult Index()
        {
     
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        [CustomAuthorizeAttribute(Roles= "SuperAdmin")]
        public ActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }



        // POST: Admin/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult RegisterAdmin(Admin ad,string password)// string password is only for comfirmation of the typed password
        {
            try
            {
                if (spa.Get(x => x.mailAdmin == ad.mailAdmin)!=null)
                {//if admin already exists 
                    ViewBag.DuplicateMessage = "mail already exists";
                    return View("RegisterAdmin");
                }
                if (ad.passwordAdmin != password)
                {// if passwords does not match
                    ViewBag.ErrorMessage = "password don't match";
                    return View("RegisterAdmin");
                }
                spa.add_Admin(ad);

                return RedirectToAction("login");
            }
            catch(Exception r)
            {
                return View();
               
            }
        }

        [HttpGet]
        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult Edit(int id)
        {
            Admin ad = new Admin();//empty admin model
            ad = spa.GetById(id);//get the admin by admin 
            ViewData.Model = ad;

            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult Edit(Admin ad)
        {
            try
            {
                spa.edit_admin_profile(ad);

                return RedirectToAction("ListAdmin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Admin ad = new Admin();
            ad = spa.GetById((long)id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                spa.delete_admin(spa.GetById(id));//check serviceAdmin
                return RedirectToAction("ListADMIN");
            }
            catch
            {
                return View();
            }
        }
        public void List_annonce()
        {

        }
        public void Accept_annonce()
        {

        }
        public void Delete_annonce()
        {

        }

        //GET:Admin/newsletter
        [Authorize]
        public ActionResult Newsletter()
        {
            MailMessage mailmessage=new MailMessage();
            return View();

        }

        
        [Authorize]
        [HttpPost]
        public ActionResult Newsletter(string obj,string body)
        {
            try
            {
                string sendermail = System.Configuration.ConfigurationManager.AppSettings["SenderMail"].ToString();
                string senderpassword= System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 1000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                MailMessage mailMessage = new MailMessage(sendermail, "karim-nar@live.fr", obj, body);
                client.Credentials = new NetworkCredential(sendermail, senderpassword);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }

            return RedirectToAction("");
        }


    }
}
