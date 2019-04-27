using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;

using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

using EventWeb.Security;
using Model;
using Service;
using Service.EventFolder;

namespace EventWeb.Controllers
{



    public class AdminController : Controller
    {
        IserviceAdmin spa = new serviceAdmin();
        //getting the instance of service that way i can use the service pattern and admin service
        IserviceLogs spl = new serviceLogs();


        public ActionResult login()
        {
            Session["AdminID"] = null;//setting the session to be null
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad, string ReturnUrl)
        {

            if (spa.authAdmin(ad.mailAdmin, ad.passwordAdmin))//check serviceAdmin
            {
                Admin _admin = new Admin();
                _admin = spa.Get(x => x.mailAdmin == ad.mailAdmin && x.passwordAdmin == ad.passwordAdmin);
                FormsAuthentication.SetAuthCookie(ad.mailAdmin, true);//store user mail in cookies 

                var cookie = new HttpCookie("cookie");
                cookie.Value = _admin.nameAdmin;
                Response.Cookies.Add(cookie);



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
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

           return RedirectToAction("login");
        }

        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult ListAdmin()
        {//only super admin can view list admins and manage admins(edit,delete)

            List<Admin> _admin = new List<Admin>();
            _admin = spa.GetMany(x => x.isSuperAdmin != true).ToList();//get you all lines in the table admin without the super admin
            ViewData.Model = _admin;

            return View();
        }

        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        // GET: Admin
        public ActionResult Index()
        {
            

            return View();
        }

        public JsonResult Univstats()
        {
            IserviceEvent spe = new serviceEvent();
            IserviceUniv spun = new serviceUniv();
            int total = spe.GetAll().Count();
            List<Univstat> univstat = new List<Univstat>(spun.GetAll().Select(x => new Univstat { id = x.idUniv, name = x.UnivName }).ToList());

            foreach (Univstat i in univstat)
            {
                i.count = spe.GetMany(x => x.hostedby.idUniv == i.id).Count();
                i.ratio = (i.count * 100) / total;
            }
            ViewBag.univstat = univstat;

            return Json(univstat.Select(x=>new { name=x.name,y=x.count} ),JsonRequestBehavior.AllowGet);
        }


        public JsonResult Eventstat()
        {
            IserviceEvent spe = new serviceEvent();
            DateTimeFormatInfo mn = new DateTimeFormatInfo();
            var eve = spe.GetAll().GroupBy(s => s.EventDate.Month).Select(s=>new {month= mn.GetAbbreviatedMonthName(s.Key),count=s.Count()}).OrderBy(s=>s.month).ToList();
            return Json(eve.Select(x => new { val= x.count, mon = (x.month) }), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Themestats()
        {
            IserviceEvent spe = new serviceEvent();
            var eve = spe.GetAll().GroupBy(s => s.theme).Select(s => new { theme = s.Key.designation, count = s.Count() });


            return Json(eve.Select(x => new { theme = x.theme, val = x.count }), JsonRequestBehavior.AllowGet);


        }



        public ActionResult profile()
        {

            int ida = spa.Get(x => x.mailAdmin == User.Identity.Name).idAdmin;

            return RedirectToAction("Edit",new { id=ida});
        }
        



        // GET: Admin/Create
        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }



        // POST: Admin/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult RegisterAdmin(Admin ad, string password)// string password is only for comfirmation of the typed password
        {
            try
            {
                if (spa.Get(x => x.mailAdmin == ad.mailAdmin) != null)
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
            catch (Exception)
            {
                return View();

            }
        }

        [HttpGet]
        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult Edit(int id)
        {
            Admin ad = new Admin();//empty admin model
            ad = spa.GetById(id);//get the admin by id 
            ViewData.Model = ad;

            return View();
        }

       
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        [ValidateAntiForgeryToken]
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


        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult Delete(int id)
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
        

       

        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult Newsletter()
        {
            MailMessage mailmessage=new MailMessage();
            return View();

        }

        
        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult Newsletter(string obj,string body)
        {
            IServiceMS sms = new ServiceMS();
            IserviceNL spnl = new serviceNL();
            try
            {
                string mails = spnl.GetAll().SelectMany(a => a.mailsubs.Split(',')).ToString();
                sms.sendMail(mails, obj, body);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }

            return RedirectToAction("");
        }


        public ActionResult logs()
        {
            List<Logs> log = new List<Logs>();
                log=spl.GetAll().ToList();
            ViewData.Model = log;
            return View();
        }



        public ActionResult loadorg(int idUniv)
        {
            IserviceOrganization spo = new serviceOrganization();
            return Json(spo.GetMany(x => x.idUniv == idUniv).Select(s => new {
                Id = s.idorg,
                Name = s.orgname
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Organizations()
        {
            IserviceUniv spu = new serviceUniv();
            List<University> listuniv = new List<University>();
            listuniv = spu.GetAll().ToList();
            ViewBag.listuniv = listuniv;
            return View();
        }

        public ActionResult AddOrganization()
        {
            IserviceUniv spu = new serviceUniv();
            List<University> listuniv = new List<University>();
            listuniv = spu.GetAll().ToList();
            ViewBag.listuniv = listuniv;
            return View();
        }
        [HttpPost]
        public ActionResult AddOrganization(organization org,int univ)
        {
            org.idUniv = univ;
            IserviceOrganization spo = new serviceOrganization();
            spo.Add(org);
            spo.Commit();

            return RedirectToAction("Organizations");
        }

    }
     public class Univstat
    {

        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public float ratio { get; set; }

    }
}

