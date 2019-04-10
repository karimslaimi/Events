using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Service.EventFolder
{
    public class serviceEvent : servicePattern<Event>, IserviceEvent
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceEvent() : base(uow)
        {

        }




        public void acceptEvent(int eventid,int idadmin)
        {
            
            Event eve = this.GetById(eventid);
            eve.adminid = idadmin;
            this.Update(eve);
            this.Commit();
            
        }

        public void create_event(Event _event)
        {
            this.Add(_event);
            this.Commit();
        }

        public void refuseEvent(int eventid)
        {
            this.Delete(this.GetById(eventid));
            this.Commit();
        }

        public void edit_event(Event _event)
        {
            Event eve = this.GetById(_event.idEvent);
            eve.Description = _event.Description;
            eve.EventDate = _event.EventDate;
            eve.EventTitle = _event.EventTitle;
            eve.adminid = null;
            this.Update(eve);
            this.Commit();
        }

        public List<Event> search_Event(string keyword)
        {
            List<Event> eve = new List<Event>();
            eve = this.GetMany(x =>
            x.Description.Contains(keyword)||
            x.EventLocation.Contains(keyword)||
            x.EventTitle.Contains(keyword)||
            x.theme.designation.Contains(keyword)            
            
            ).ToList();
            return eve;
        }

        public List<Event> search_event_date(DateTime date)
        {
            List<Event> eve = new List<Event>();
            eve = this.GetMany(x => x.EventDate==date).ToList();
            return eve;
        }

        public List<Event> search_event_location(string location)
        {
            List<Event> eve = new List<Event>();
            eve = this.GetMany(x => x.EventLocation.Contains(location)).ToList();
            return eve;
        }

        public List<Event> search_event_theme(string theme)
        {
            List<Event> eve = new List<Event>();
            eve=this.GetMany(x=>x.theme.designation.Contains(theme)).ToList();
            return eve;
        }
    }
}
