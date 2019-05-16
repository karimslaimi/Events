using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Data;
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
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad, string ReturnUrl)
        {

            if (spa.authAdmin(ad.mailAdmin, ad.passwordAdmin))//check serviceAdmin
            {
                Admin _admin = new Admin();
                FormsAuthentication.SetAuthCookie(ad.mailAdmin, true);//store user mail in cookies 
             

                return RedirectToAction("index");
            }



            return View();
        }

        [Authorize]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            
       

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

        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult Index()
        {
            

            return View();
        }

        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public JsonResult stats()
        {
            IserviceEvent spe =new ServiceEvent();
            IserviceUniv spun = new serviceUniv();
            IserviceTheme spt = new serviceTheme();

            List<dynamic> list = new List<dynamic>();
            var eve = spe.Eventstat();
            var univ = spun.Univstat();
            var them = spt.Themestat();
            list.Add(eve);
            list.Add(univ);
            list.Add(them);
            

            return Json(list, JsonRequestBehavior.AllowGet);

            

    
        }

       


        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult profile()
        {

            int ida = spa.Get(x => x.mailAdmin == User.Identity.Name).idAdmin;

            return RedirectToAction("Edit",new { id=ida});
        }
        



       
        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
        public ActionResult RegisterAdmin()
        {
            return View("RegisterAdmin");
        }



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

                return RedirectToAction("index");
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
            else
            {
                spa.delete_admin(ad);//check serviceAdmin
                return RedirectToAction("ListADMIN");

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

        [CustomAuthorizeAttribute(Roles = "SuperAdmin")]
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

        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult Organizations()
        {
            IserviceUniv spu = new serviceUniv();
            List<University> listuniv = new List<University>();
            listuniv = spu.GetAll().ToList();
            ViewBag.listuniv = listuniv;
            return View();
        }

        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult AddOrganization()
        {
            IserviceUniv spu = new serviceUniv();
            List<University> listuniv = new List<University>();
            listuniv = spu.GetAll().ToList();
            ViewBag.listuniv = listuniv;
            return View();
        }

        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
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

