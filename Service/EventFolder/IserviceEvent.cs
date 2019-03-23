using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EventFolder
{
   public  interface IserviceEvent:IservicePattern<Event>
    {

        void edit_event(Event _event);
        void delete_event(Event _event);
        void create_event(Event _event);
        List<Event> search_Event(string keyword);
        List<Event> search_event_date(DateTime date);
        List<Event> search_event_location(string location);
        List<Event> search_event_theme(string theme);


    }
}
