using Service.EventFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using EventWeb.Security;
using System.Security.Principal;
using System.Net.Http;
using Service;
using Service.Univ;
using Data;

namespace EventWeb.Controllers
{
    public class EventController : Controller
    {


        IserviceEvent spe = new serviceEvent();
        IserviceUser spu = new serviceUser();
        IserviceOrganization spo = new serviceOrganization();
        IserviceUniversity spun = new serviceUniversity();
        DatabContext ctx = new DatabContext();
        




        // GET: Event
        public ActionResult Index()
        {
            //list of events
            List<Event> _event = new List<Event>();

            _event = spe.GetMany(x=>x.approvedBy!=null).ToList();
            return View();
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
            List<University> univlist = new List<University>();
            univlist = ctx.University.ToList();//spun service te3ha
            ViewBag.univlist = univlist;
            
            return View();
        }

        public ActionResult loadorg(int idUniv)
        {
            return Json(ctx.organization.Where(x=>x.university.idUniv==idUniv).Select(s => new {
                Id = s.idorg,
                Name = s.orgname }).ToList() ,JsonRequestBehavior.AllowGet);
        }

        // POST: Event/Create
        [CustomAuthorizeAttribute(Roles = "User")]
        [HttpPost]
        public ActionResult Create(Event _event)
        {
            try
            {
                if (ModelState.IsValid){
                    _event.approvedBy = null;
                    _event.CreationDate = new DateTime();
                    _event.creator = spu.Get(x=>x.username==Session["Username"].ToString());
                   // _event.hostedby

                }
                //creation date
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
    }
}
