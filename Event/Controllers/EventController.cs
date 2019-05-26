using Service.EventFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Model;
using EventWeb.Security;
using Service;

using System.Web;
using System.IO;

namespace EventWeb.Controllers
{
    public class EventController : Controller
    {


        IserviceEvent spe = new ServiceEvent();
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
            _event = spe.GetMany(x => x.adminid != null && x.EventDate >= DateTime.Today).ToList();

            return View(_event);
        }
        public ActionResult search(int? Univ, int? them, string keyword, DateTime? date)
        {
            List<Event> _event = new List<Event>();
            if (!string.IsNullOrEmpty(keyword))
            {
                _event.AddRange(spe.search_Event(keyword).ToList());
            }
            if (Univ != null)
            {
                _event.AddRange(spe.search_event_university(Univ.GetValueOrDefault()));
            }
            if (them != null)
            {
                _event.AddRange(spe.search_event_theme(them.GetValueOrDefault()));
            }
            if (date != null)
            {
                _event.AddRange(spe.search_event_date(date.GetValueOrDefault()));
            }
            if (_event.Count()==0)
            {
                _event = spe.GetMany(x => x.adminid != null && x.EventDate >= DateTime.Today).OrderBy(x=>x.EventDate).ToList();
            }
            return View("Index", _event);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            IServiceUserEvent spue = new serviceUserEvent();

            Event _event = spe.GetById(id);
            List<EventPicture> pic = sep.GetMany(x => x.eventid == id).ToList();
           
            ViewBag.participate = false;
            if (User.Identity.IsAuthenticated && spa.Get(x=>x.mailAdmin==User.Identity.Name)==null) {
                int uid = spu.Get(x => x.username == User.Identity.Name).id;
                if(spue.Get(x => x.eventid == id && x.userid == uid)!=null)
                ViewBag.participate = spue.Get(x => x.eventid == id && x.userid == uid).participation;
            }

            ViewBag.like = false;
            if (User.Identity.IsAuthenticated && spa.Get(x => x.mailAdmin == User.Identity.Name) == null)
            {
                int uid = spu.Get(x => x.username == User.Identity.Name).id;
                if (spue.Get(x => x.eventid == id && x.userid == uid) != null)
                    ViewBag.like = spue.Get(x => x.eventid == id && x.userid == uid).like;
            }

            if (_event.hostedby == null)
            {
                ViewBag.hostedby = spo.GetById((long)_event.hostedbyid).orgname;
            }

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
            if (files.First()==null ||  verifyFiles(files))
            {
                try
                {
                    
                    
                    _event.creatorid = spu.Get(x => x.username == User.Identity.Name).id;
                    spe.create_event(_event);
                    if (files.First() != null)
                    {
                        var path = "";
                        int i = 1;
                        foreach (var item in files)
                        {
                            string name = "id" + _event.idEvent + "im" + i + Path.GetExtension(item.FileName);
                            EventPicture image = new EventPicture();

                            path = Path.Combine(Server.MapPath("../Content/eventpics/"), name);
                            item.SaveAs(path);
                            image.eventid = _event.idEvent;
                            image.picName = name;
                            sep.Add(image);
                            sep.Commit();
                            i++;

                        }
                    }

                  

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
                ModelState.AddModelError(string.Empty,"check files extension or size or count only (png,jpg,jpeg,bmp) files and not mere than 4 pictures and the size of each one maximum of 4mb");
                return View(_event);
            }
        }


        [CustomAuthorizeAttribute(Roles ="User")]
        [HttpPost]
        public JsonResult Participate(int ide)
        {
           
            int? uid1 = spu.Get(x => x.username == User.Identity.Name).id;
            if (uid1 != null)
            {
                int uid = uid1.GetValueOrDefault();
                IServiceUserEvent spue = new serviceUserEvent();
                UserEvent uev = new UserEvent();
                uev = spue.Get(x => x.userid == uid && x.eventid == ide);
                if (uev == null)
                { // if the user didn't participate or like before
                    spue.participate(uid, ide);
                    return Json(new { IsOk = true, Eventid = ide, Action = "participated" });
                }
                else
                {
                    if (uev.participation == false)
                    {
                        uev.participation = true;
                        spue.Update(uev);
                        spue.Commit();
                        return Json(new { IsOk = true, Eventid = ide, Action = "participated" });

                    }
                    else
                    {
                        uev.participation = false;
                        spue.Update(uev);
                        spue.Commit();
                        return Json(new { IsOk = true, Eventid = ide, Action = "participate" });
                    }

                }

            }else return Json(new { IsOk = true, Eventid = ide, Action = "false" });
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

        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "User")]
        [HttpPost]
        public ActionResult Edit(Event _event)
        {
            if (spu.Get(x => x.username == User.Identity.Name).id == spe.GetById(_event.idEvent).creatorid)
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
            if (spu.Get(x => x.username == User.Identity.Name).id == spe.GetById(id).creatorid)
            {

                spe.Delete(spe.GetById(id));
                return RedirectToAction("index","Home");

            }
            else
            {
                return RedirectToAction("index","Home");
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
           
            

            IServiceMS spsms = new ServiceMS();
            IServiceUserEvent spue = new serviceUserEvent();
            string mails = spue.GetMany(x => x.participation == true).SelectMany(x => x.User.mail.Split(',')).ToString();
            if (mails != null)
            {
                spsms.sendMail(mails, "l'évenement " + spe.GetById(id).EventTitle + "a été supprimer", "l'annonce de cet evenement a été suprrimer verifier avec les organizateurs de cet evenement");
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
            IServiceMS spsms = new ServiceMS();
            IServiceUserEvent spue = new serviceUserEvent();
            string mails = spue.GetMany(x => x.participation == true).SelectMany(x => x.User.mail.Split(',')).ToString();
            if (mails != null)
            {
                spsms.sendMail(mails,"l'évenement "+spe.GetById(id).EventTitle+"a été modifier consulter le lien ci dessous pour voir les changements","localhost:8080/Event/Details/"+id);
            }
            
            return RedirectToAction("EventNotApproved");
        }



        [CustomAuthorizeAttribute(Roles = "User")]
        [HttpPost]
        public JsonResult Like(int ide)
        {
            IServiceUserEvent spue = new serviceUserEvent();
            int? uid1 = spu.Get(x => x.username == User.Identity.Name).id;
            UserEvent uev = new UserEvent();
            if (uid1 != null)
            {
                int uid = uid1.GetValueOrDefault();
                uev = spue.Get(x => x.userid == uid && x.eventid == ide);
                if (uev == null)
                { // if the user didn't like the event before
                    spue.like(uid, ide);
                    return Json(new { IsOk = true, Eventid = ide, Action = "liked" });
                }
                else
                {

                    if (uev.like == false)
                    {
                        uev.like = true;
                        spue.Update(uev);
                        spue.Commit();
                        return Json(new { IsOk = true, Eventid = ide, Action = "liked" });
                    }
                    else
                    {
                        uev.like = false;
                        spue.Update(uev);
                        spue.Commit();
                        return Json(new { IsOk = true, Eventid = ide, Action = "unlike" });
                    }

                }
            }
            else return Json(new { IsOk = true, Eventid = ide, Action = "false" });

        }

        [CustomAuthorizeAttribute(Roles = "User")]
        public ActionResult MyEvents()
        {
            int uid = spu.Get(x => x.username == User.Identity.Name).id;
            List<Event> eve = spe.GetMany(x => x.creatorid == uid).ToList();
            return View(eve);
        }




        public PartialViewResult Share(int id)
        {
            ViewBag.id = id;

            return PartialView(id);
        }



    }
}
