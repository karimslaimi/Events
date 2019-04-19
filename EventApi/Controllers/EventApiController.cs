using Model;
using Service.EventFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventApi.Controllers
{
    public class EventApiController : ApiController
    {

        IserviceEvent spe = new serviceEvent();

        public IEnumerable<Event> GetEvents()
        {
            return spe.GetAll();
        }


    }
}
