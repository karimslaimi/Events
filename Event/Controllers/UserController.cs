using Service;
using System.Web.Mvc;
using EventWeb.Security;
using Model;
using System.Web.Security;
using System.Linq;
using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Web;

namespace EventWeb.Controllers
{
    public class UserController : Controller
    {

        IserviceUser spu = new serviceUser();
        // GET: User
        public ActionResult Index()
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
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random rand = new Random();
                _user.activated = new string(Enumerable.Repeat(chars, 36).Select(s => s[rand.Next(36)]).ToArray());
                spu.register_user(_user);
                SendVerificationMail(_user.id, _user.activated);
                return RedirectToAction("index");
            }
            else
            {
                return View(_user);
            }


        }

        public ActionResult verifymail(int id, string key)
        {
            User _user = new User();
            _user = spu.GetById(id);
            if (_user != null && key == _user.activated)
            {
                _user.activated = "active";
                spu.Update(_user);
                spu.Commit();
                return View();
            }
            else
            {
                return RedirectToAction("index");
            }

        }

        public void SendVerificationMail(int id, string key)
        {
            try
            {
                string sendermail = System.Configuration.ConfigurationManager.AppSettings["SenderMail"].ToString();
                string senderpassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 1000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                MailMessage mailMessage = new MailMessage(sendermail, spu.GetById(id).mail, "verify your mail", "http://localhost:8080/User/verifymail/?id=" + id + "&key=" + key);
                client.Credentials = new NetworkCredential(sendermail, senderpassword);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

            }
            catch (Exception)
            {

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
            if (ModelState.IsValid && spu.AuthUser(_user.username, _user.password))
            {//check if the user model is valid
                if (spu.Get(x => x.username == _user.username).activated != "active")
                {
                    ViewBag.ErrorMessage = "verify your mail";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(_user.username, false);//add username to cookies
                    this.Session["UserId"] = spu.Get(x => x.username == _user.username).id;//store the id of the user in the session
                    this.Session["Username"] = _user.username.ToString();
                    return RedirectToAction("index");
                }
            }
            return View("Index");

        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index");
        }

        [CustomAuthorizeAttribute(Roles = "User")]
        public ActionResult Edit(int id)
        {
            User us = new User();
            us = spu.GetById(id);
            us.password = "";//make the passsword empty that way the user won't be able to see the password
            ViewData.Model = us;//put the mode in the view 
            //the user is able only to change his first and last name ,email,phone number and password he can't change his birth of date and username
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "User")]
        public ActionResult Edit(User _user)
        {
            if (ModelState.IsValid && _user.password == spu.GetById(_user.id).password)
            {
                spu.edit_user_profile(_user);//check serviceUser
            }
            else
            {

                return View();
            }
            return RedirectToAction("index");
        }







        public static async Task passwordsms(string phone,string key) {

            const string account = "ip1-16643";
            const string apiKey = "EME8Zwc8DCygoUEyNvYp";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.ip1sms.com");

                // Setting authentication
                byte[] authBytes = Encoding.UTF8.GetBytes($"{account}:{apiKey}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

                // Setting user agent
                var version = typeof(UserController).Assembly
                    .GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("IP1.Example", version));




                dynamic sms = new ExpandoObject();

                sms.Numbers = new List<string>() { "+216" + phone };
                sms.Message = "votre code est :" + key;
                sms.From = "karim";


                // Serialize the object into JSON
                var json = JsonConvert.SerializeObject(sms);
                StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/sms/send", requestContent);

            }




        }

        
        //this methode will redirect the user where he can put his credentials mail or phone number
        public ActionResult forgotPassword()
        {
            return View();
        }

        //this methode will recover the credentials i won't give the user an information if the credentials are correct
        public ActionResult recoverPassword(string credentials)
                        
        {
           

            User _user = spu.Get(x=>x.phone==credentials||x.phone==credentials);
            if (_user != null)
            {
                const string chars = "0123456789";
                Random rand = new Random();
                string key = new string(Enumerable.Repeat(chars, 6).Select(s => s[rand.Next(10)]).ToArray());

                var cookie = new HttpCookie("cookie");
                cookie.Value = key;
                Response.Cookies.Add(cookie);
                
                Session["User"]= _user.phone;
               var  task=passwordsms(_user.phone, key);
               // task.Wait();

                return View();
            }
            else
            {
                Session["resetkey"] = null;
                return View();

            }
        }

        //this will verify the code given in the view redirect to NewPassword if correct or return the same view with error
        public ActionResult verifyCode(string code)
        {
            if ()
            {
                return RedirectToAction("NewPassword");
            }
            else
            {
                ViewBag.ErrorMessage = "code incorrect";


                return RedirectToAction("recoverPassword");
            }
            
        }

        //it will return the view to put the new password
        public ActionResult NewPassword()
        {

            User _user = spu.Get(x=>x.phone==Session["User"].ToString());
            ViewBag.mail = _user.mail;
            return View(_user);

        }

        //this will update the database omg it took more than all the user methodes
        public ActionResult ValidatePassword(User _user)
        {

            User us = spu.GetById(_user.id);
            us.password = _user.password;
            spu.Update(us);
            spu.Commit();

            return RedirectToAction("login");
        }



    }
}
