using Service.EventFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model;
using EventWeb.Security;
using Service;

using Service.Themes;
using System.Web;
using System.IO;

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
        IserviceLogs spl = new serviceLogs();
        IEventPictures sep = new EventPictures();


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
            Event _event = spe.GetById(id);
            List<EventPicture> pic = sep.GetMany(x => x.eventid == id).ToList();
           // var img=pic.Where()
            ViewData.Model = _event;
            ViewBag.pic = pic;
            return View();
        }

        [HttpGet]
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

        protected bool verifyFiles(List<HttpPostedFileBase> files)
        {
            bool flag = true;
            if (files.Count() < 5 && files.Count() > 0)
            {
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        if (item.ContentLength > 0 && item.ContentLength< 5000000)
                        {
                            if(!(Path.GetExtension(item.FileName).ToLower()==".jpg"||
                                Path.GetExtension(item.FileName).ToLower() == ".png" ||
                                Path.GetExtension(item.FileName).ToLower() == ".bmp" ||
                                Path.GetExtension(item.FileName).ToLower() == ".jpeg"))
                            {
                                flag = false;break;
                            }



                                

                        }
                        else { flag = false;break; }
                    }
                    


                }
            }
            else { flag = false;  }

            return flag;
        }


       
        [CustomAuthorizeAttribute(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event _event,int theme,int hostedby,List<HttpPostedFileBase> files)
        {
            _event.themeid = theme;
            _event.hostedbyid = hostedby;
            _event.adminid = null;
            _event.CreationDate = DateTime.Now;
            if (verifyFiles(files))
            {
                try
                {

                    
                    _event.creatorid = spu.Get(x => x.username == User.Identity.Name).id;
                    spe.create_event(_event);
                    var path = "";
                    int i = 1;
                    foreach(var item in files)
                    {
                        string name = "id" + _event.idEvent + "im" + i + Path.GetExtension(item.FileName);
                        EventPicture image = new EventPicture();
                        
                        path = Path.Combine(Server.MapPath("../Content/eventpics/"),name );
                        item.SaveAs(path);
                        image.eventid = _event.idEvent;
                        image.picName = name;
                        sep.Add(image);
                        sep.Commit();
                        i++;

                    }

                    //
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "error occured try again later";
                    return RedirectToAction("Create");
                }
            }
            else
            {
                List<University> listuniv = new List<University>();
                listuniv = spun.GetAll().ToList();
                ViewBag.listuniv = listuniv;
                List<Theme> themelist = new List<Theme>();
                themelist = spt.GetAll().ToList();
                ViewBag.themelist = themelist;
                ViewBag.Error = "check file extension or size or count only (png,jpg,jpeg,bmp) files and not mere than 4 pictures and the size of each one maximum of 4mb";
                return View(_event);
            }
        }


        [CustomAuthorize(Roles ="User")]
        public ActionResult Edit(int id)
        {
            if (spu.Get(x => x.username== User.Identity.Name).id == spe.GetById(id).creatorid)
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


        [CustomAuthorize(Roles = "Admin,SuperAdmin")]
        public ActionResult RefuseEvent(int id)
        {
            
            List<EventPicture> pics = sep.GetMany(x=>x.eventid==id).ToList();
            foreach(var item in pics)
            {
               // if (System.IO.File.Exists(Request.MapPath(@"../Content/eventpics/" + item.picName)))
               // {
                    System.IO.File.Delete(Request.MapPath(@"/Content/eventpics/" + item.picName));

                //}
                sep.Delete(item);
                sep.Commit();


            }
           
            spe.refuseEvent(id);
          
         

            return RedirectToAction("EventNotApproved");
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
            Admin _admin = spa.Get(x => x.mailAdmin == User.Identity.Name);

            spe.acceptEvent(id,_admin.idAdmin);
            Logs log = new Logs();
            log.adminid = _admin.idAdmin;
            log.eventid = id;
            log.date = DateTime.Now;
            spl.Add(log);
            spl.Commit();
          
            return RedirectToAction("EventNotApproved");
        }










    }
}
