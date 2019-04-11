using Service.EventFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model;
using EventWeb.Security;
using Service.Univ;
using Service;
using Service.Themes;


namespace EventWeb.Controllers
{
    public class EventController : Controller
    {


        IserviceEvent spe = new serviceEvent();
        IserviceUniv spun = new serviceUniv();
        IserviceOrganization spo = new serviceOrganization();
        IserviceUser spu = new serviceUser();
        IserviceTheme spt = new serviceTheme();
        IserviceAdmin spa = new serviceAdmin();


        // GET: Event
        public ActionResult Index()
        {
            //list of events
            List<Event> _event = new List<Event>();

            _event = spe.GetMany(x=>x.approvedBy!=null).ToList();
           
            return View(_event);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        [CustomAuthorizeAttribute(Roles = "User")]
        public ActionResult Create()
        {
            List<University> listuniv = new List<University>();
            listuniv = spun.GetAll().ToList();
            ViewBag.listuniv = listuniv;
            List<Theme> themelist = new List<Theme>();
            themelist = spt.GetAll().ToList();
            ViewBag.themelist = themelist;
            
            return View();
        }

        
        public ActionResult loadorg(int idUniv)
        {
            return Json(spo.GetMany(x=>x.university.idUniv==idUniv).Select(s => new {
                Id = s.idorg,
                Name = s.orgname }).ToList() ,JsonRequestBehavior.AllowGet);
        }

       
        [CustomAuthorizeAttribute(Roles = "User")]
        [HttpPost]
        public ActionResult Create(Event _event,int theme,int hostedby)
        {
            
            try
            {
                    _event.themeid = theme;
                    _event.hostedbyid = hostedby;
                    _event.adminid = null;
                    _event.CreationDate = DateTime.Now;
                    _event.creatorid = spu.Get(x=>x.username==User.Identity.Name).id;
                    
                    spe.create_event(_event);

                
                
                //
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [CustomAuthorize(Roles ="User")]
        public ActionResult Edit(int id)
        {
            if (spu.Get(x => x.mail == User.Identity.Name).id == spe.GetById(id).creatorid)
            {
                return View(spe.GetById(id));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [CustomAuthorize(Roles = "User")]
        [HttpPost]
        public ActionResult Edit(Event _event)
        {
            if (spu.Get(x => x.mail == User.Identity.Name).id == spe.GetById(_event.idEvent).creatorid)
            {
                try
                {
                    spe.edit_event(_event);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        [CustomAuthorize(Roles = "User")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (spu.Get(x => x.mail == User.Identity.Name).id == spe.GetById(id).creatorid)
            {

                spe.Delete(spe.GetById(id));
                return RedirectToAction("index");

            }
            else
            {
                return RedirectToAction("index");
            }
        }


        [CustomAuthorize(Roles = "User")]
        public ActionResult RefuseEvent(int id)
        {

            spe.refuseEvent(id);
            return RedirectToAction("index");
        }

        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult EventNotApproved()
        {
            List<Event> _eventnotapproved = spe.GetMany(x => x.approvedBy==null).ToList();

            return View(_eventnotapproved);
        }

        
        [CustomAuthorizeAttribute(Roles = "SuperAdmin,Admin")]
        public ActionResult AcceptAnnonce(int id)
        {

            spe.acceptEvent(id,spa.Get(x => x.mailAdmin == User.Identity.Name).idAdmin);
            return RedirectToAction("EventNotApproved");
        }






    }
}
