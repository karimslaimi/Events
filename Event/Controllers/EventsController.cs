using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model;
using Service.EventFolder;

namespace EventWeb.Controllers
{

    public class EventsController : ApiController
    {
        IserviceEvent spe = new ServiceEvent();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        // GET: api/Events
        public JsonResult Get()
        {


            IEnumerable<Event> list = spe.GetMany(x => x.adminid != null).ToList();

            return new JsonResult()
            {
                Data = list,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = 86753090
            };
        }

        // GET: api/Events/5
        public IHttpActionResult Get(int id)
        {
            var obj = spe.GetById(id);
            return Ok(new { results = obj });
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
    }
}
