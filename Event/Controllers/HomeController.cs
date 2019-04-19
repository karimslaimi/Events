
using System.Web.Mvc;
using System.Web.Http;

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
            return View();
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