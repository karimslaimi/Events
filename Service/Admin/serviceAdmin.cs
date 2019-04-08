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
using System.Web.Security;

namespace Service
{
    public class serviceAdmin : servicePattern<Admin>, IserviceAdmin

        //this implementation implements the interface IserviceAdmin and inherit from the servicePattern implementation with admin cast
        //with the inheritance of the service Pattern i have already the implementation of the crud operations on the type Admin
        //and i can now use the crud operations to implement the Admin services
        //the same principle in the other services 
        //convention over configuration
        
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceAdmin() : base(uow)
        {

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
            this.Delete(_admin);
            this.Commit();
        }

        public void delete_comment(UserEvent _user_event)
        {
            throw new NotImplementedException();
        }

        



        public void edit_admin_profile(Admin _admin)
        {

            Admin ad = this.GetById(_admin.idAdmin);
            if (!string.IsNullOrWhiteSpace(_admin.nameAdmin) && !string.IsNullOrEmpty(_admin.nameAdmin))
            {
                ad.nameAdmin = _admin.nameAdmin;
            }
            if (!string.IsNullOrWhiteSpace(_admin.mailAdmin) && !string.IsNullOrEmpty(_admin.mailAdmin))
            { 

            ad.mailAdmin = _admin.mailAdmin;

            } 
            if (!string.IsNullOrEmpty(_admin.passwordAdmin)&&!string.IsNullOrWhiteSpace(_admin.passwordAdmin))
            {
                ad.passwordAdmin = _admin.passwordAdmin;
            }
            this.Update(ad);
            this.Commit();
            
        }

       
        public List<Event> Event_log()
        {
            throw new NotImplementedException();
        }

     

        

        
    }
}
