using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Event.Security;
using Model;
using System.Web.Security;

namespace Event.Controllers
{
    public class UserController : Controller
    {

        IserviceUser spu = new serviceUser();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User _user)
        {

            if (ModelState.IsValid)
            {
                spu.Add(_user);
                return RedirectToAction("index");
            }
            else
            {
                return View(_user);
            }

            
        }




        // GET: User/Create
        public ActionResult login()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult login(User _user)
        {
            if (ModelState.IsValid && spu.AuthUser(_user.username,_user.password)) {//check if the user model is valid
                FormsAuthentication.SetAuthCookie(_user.username, false);//add username to cookies
                Session["UserId"] = spu.Get(x => x.username == _user.username).id;//store the id of the user in the session

            }
                return View("Index");
            
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index");
        }


        public ActionResult Edit(int id)
        {
            User us = new User();
            us = spu.GetById(id);
            ViewData.Model = us;//put the mode in the view 
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(User _user)
        {
            if (ModelState.IsValid)
            {
                spu.edit_user_profile(_user);//check serviceUser
            }
            return View();
        }

       
    }
}
