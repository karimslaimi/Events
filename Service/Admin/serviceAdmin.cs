using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model;
using Infrastructure;
using Data.Infrastructure;
using MyFinance.Data.Infrastructure;

namespace Service
{
    public class serviceAdmin : servicePattern<Admin>,IserviceAdmin


    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceAdmin() : base(uow)
        {

        }

        public void accept_event(Event annonce)
        {
            throw new NotImplementedException();
        }

        

        public void add_Admin(Admin _admin)
        {
            _admin.isSuperAdmin = false;
            this.Add(_admin);
            this.Commit();
        }



        public bool authAdmin(string login, string password)
        {
            return this.Get(x => x.mailAdmin == login && x.passwordAdmin == password) != null;
        }

      


        

        public void delete_admin(Admin _admin)
        {
            throw new NotImplementedException();
        }

        public void delete_comment(UserEvent _user_event)
        {
            throw new NotImplementedException();
        }

        public void delete_event(Event annonce)
        {
            throw new NotImplementedException();
        }

       

        public void edit_admin_profile(Admin _admin)
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
