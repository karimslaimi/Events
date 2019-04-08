using Service.EventFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using EventWeb.Security;
using Service.Univ;
using Service;
using Service.Themes;
using Microsoft.AspNet.Identity;

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

        // GET: Event/Create
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

        // POST: Event/Create
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

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/5
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

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
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
