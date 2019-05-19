
using System.Web.Mvc;
using System.Web.Http;
using Service.EventFolder;
using System.Linq;
using Service;
using Model;
using System;

namespace EventWeb.Controllers
{
    public class HomeController :Controller
    {
        IserviceUniv spun = new serviceUniv();
        IserviceEvent spe = new ServiceEvent();
        IserviceTheme spt = new serviceTheme();

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Index()
        {
         
            var eve = spe.GetMany(x=>x.adminid!=null && x.EventDate >= DateTime.Today).OrderBy(x=>x.UserEvent.Count()).Take(4);
            ViewBag.listuniv = spun.GetAll().ToList();

            ViewBag.themelist = spt.GetAll().ToList();
            
            return View(eve);
        }

        public ActionResult About()
        {
          

            return View();
        }
        public ActionResult subscribe(string email)
        {
            IserviceNL spnl = new serviceNL();
            if (spnl.Get(x => x.mailsubs == email) == null)
            {
                Subscribers mnl = new Subscribers();
                mnl.mailsubs = email;
                spnl.Add(mnl);
                spnl.Commit();
            }
            return RedirectToAction("index");
        }

        

    }
}