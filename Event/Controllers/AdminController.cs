using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Event.Security;
using Model;
using Service;

namespace Event.Controllers
{

    

    public class AdminController : Controller
    {
        IserviceAdmin spa = new serviceAdmin();

        string ReturnUrl = "index";


        public ActionResult login()
        {
            Session["AdminID"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad,string ReturnUrl)
        {

            if(spa.authAdmin(ad.mailAdmin,ad.passwordAdmin))
            {
                FormsAuthentication.SetAuthCookie(ad.mailAdmin, false);
                Admin _admin = (spa.Get(x => x.mailAdmin == ad.mailAdmin));
                Session["AdminID"] = _admin.idAdmin;
                Session["mailAdmin"] = _admin.mailAdmin;
                if (_admin.isSuperAdmin)
                {
                    Session["Role"] = "SuperAdmin";
                    HttpContext.Session["Role"] = Session["Role"];
                }
                else
                {
                    Session["Role"] = "Admin";
                    HttpContext.Session["Role"] = Session["Role"];
                }

                return RedirectToAction(ReturnUrl,"/");
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
        {

            List<Admin> _admin = new List<Admin>();
            _admin = spa.GetMany(x => x.isSuperAdmin != true).ToList();
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
        public ActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }

        // POST: Admin/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegisterAdmin(Admin ad,string password)
        {
            try
            {
                if (spa.Get(x => x.mailAdmin == ad.mailAdmin)!=null)
                {
                    ViewBag.DuplicateMessage = "mail already exists";
                    return View("RegisterAdmin");
                }
                if (ad.passwordAdmin != password)
                {
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
        public ActionResult Edit(int id)
        {
            Admin ad = new Admin();
            ad = spa.GetById(id);
            ViewData.Model = ad;

            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
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
        public ActionResult Delete(int id)
        {

            Admin ad = new Admin();
            ad = spa.GetById(id);
            spa.delete_admin(ad);
            return RedirectToAction("ListAdmin");
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
