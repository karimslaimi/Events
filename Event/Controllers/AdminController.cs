using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model;
using Service;

namespace Event.Controllers
{

 // esme3 bechway fama 7keya o5ra nikomha te3 login taw nchouf m3aha fama faza hethy 

    public class AdminController : Controller
    {
        IserviceAdmin spa = new serviceAdmin();



       
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad)
        {

            if(spa.authAdmin(ad.mailAdmin,ad.passwordAdmin))
            {
                return RedirectToAction("index");
            }



            return View();
        }


        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


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
            return View("register");
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
                    return View("register");
                }
                spa.add_Admin(ad);

                return RedirectToAction("login");
            }
            catch(Exception r)
            {
                return View();
                Console.WriteLine(r);
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
