using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IserviceUser
    {
        bool AuthUser(string username, string password);
        void register_user(User _user);
        void edit_user_profile(User _user);
        void edit_event(Event _event);
        void delete_event(Event _event);
        void create_event(Event _event);
        void add_comment(User_Event comment);
        void delete_comment(User_Event comment);

    }
}
