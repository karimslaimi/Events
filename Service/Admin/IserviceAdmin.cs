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
        //this interface inherit from the service pattern interface with cast of Admin
        //hence this interface hace the simple crud operations in the entity type Admin
        //and it defines the interfaces of the other services needed

        

        void add_Admin(Admin _admin);
        bool authAdmin(string login, string password);
        
        void delete_admin(Admin _admin);
        void edit_admin_profile(Admin _admin);
        List<Event> Event_log();
        void delete_comment(UserEvent _user_event);
    }
}
