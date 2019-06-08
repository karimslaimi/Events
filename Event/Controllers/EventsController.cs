using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IdentityModel.Tokens.Jwt;
using Model;
using Service;
using Service.EventFolder;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.Web.Security;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace EventWeb.Controllers
{

    public class EventsController : ApiController
    {
        IserviceEvent spe = new ServiceEvent();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        // GET: api/Events
        [HttpGet]
      
        [Route("api/Events")]
        public List<Event> GetEvents()
        {


            List<Event> list = spe.GetMany(x => x.adminid != null).ToList();

            return list;
        }

        // GET: api/Events/5
        public Event Get(int id)
        {
            Event obj = spe.GetById(id);
            return obj;
        }

        // POST: api/Events
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Events/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Events/5
        public void Delete(int id)
        {
        }

        [Route("api/Events/Login")]
        [HttpGet]
        public bool Login([FromBody]dynamic _user)
        {
            IserviceUser spu = new serviceUser();

            SHA256 hash = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(_user.password.ToString());
            Byte[] encodedBytes = hash.ComputeHash(originalBytes);
            _user.password = BitConverter.ToString(encodedBytes);


            if (spu.AuthUser(_user.username.ToString(), _user.password.ToString()))
            {

                FormsAuthentication.SetAuthCookie(_user.username.ToString(), false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        [Authorize]
        [Route("api/Events/MyEvents")]
        [HttpGet]
        public List<Event> MyEvents()
        {
            List<Event> eve = spe.GetMany(x => x.creator.username == User.Identity.Name).ToList();
            return eve;
        }






    }

}