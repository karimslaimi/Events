using System;
using System.Collections.Generic;
using Model;
using Infrastructure;
using Data.Infrastructure;
using MyFinance.Data.Infrastructure;


namespace Service
{
    public class serviceAdmin : servicePattern<Admin>, IserviceAdmin

        
        
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
