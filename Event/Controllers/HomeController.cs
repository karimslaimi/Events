
using System.Web.Mvc;
using System.Web.Http;
using Service.EventFolder;

namespace EventWeb.Controllers
{
    public class HomeController :Controller
    {
        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Index()
        {
            IserviceEvent spe = new serviceEvent();
            var eve = spe.GetAll();
            return View("~/Views/Event/Index.cshtml",eve);
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