using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class serviceAdmin : IserviceAdmin
    {
        public void accept_event(Event annonce)
        {
            throw new NotImplementedException();
        }

        public bool authAdmin(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void delete_event(Event annonce)
        {
            throw new NotImplementedException();
        }

        public void edit_profile(User usr)
        {
            throw new NotImplementedException();
        }

        public List<Event> Event_log()
        {
            throw new NotImplementedException();
        }
    }
}
