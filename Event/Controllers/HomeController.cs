
using System.Web.Mvc;
using System.Web.Http;
using Service.EventFolder;
using System.Linq;
using Service;

namespace EventWeb.Controllers
{
    public class HomeController :Controller
    {
        IserviceUniv spun = new serviceUniv();
        IserviceEvent spe = new serviceEvent();
        IserviceTheme spt = new serviceTheme();

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Index()
        {
         
            var eve = spe.GetMany(x=>x.adminid!=null).Take(4);
            ViewBag.listuniv = spun.GetAll().ToList();

            ViewBag.themelist = spt.GetAll().ToList();
            
            return View(eve);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}