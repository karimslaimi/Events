using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IserviceAdmin:IservicePattern<Admin>
    {
        

        void add_Admin(Admin _admin);
        bool authAdmin(string login, string password);
        void accept_event(Event _event);
        void delete_event(Event _event);
        void delete_admin(Admin _admin);
        void edit_admin_profile(Admin _admin);
        List<Event> Event_log();
        void delete_comment(UserEvent _user_event);
    }
}
