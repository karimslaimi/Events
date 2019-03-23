using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public interface IserviceUser:IservicePattern<User>
    {
        bool AuthUser(string username, string password);
        void register_user(User _user);
        void edit_user_profile(User _user);
       
      

    }
}
