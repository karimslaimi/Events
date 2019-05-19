using System;
using System.Collections.Generic;
using Model;
using Infrastructure;
using Data.Infrastructure;
using MyFinance.Data.Infrastructure;
using Service.EventFolder;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
            SHA256 hash = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(_admin.passwordAdmin);
            Byte[] encodedBytes = hash.ComputeHash(originalBytes);
            _admin.passwordAdmin= BitConverter.ToString(encodedBytes);
            this.Add(_admin);
            this.Commit();
        }


        public bool authAdmin(string login, string password)
        {

            SHA256 hash = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = hash.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes);
            return this.Get(x => x.mailAdmin == login && x.passwordAdmin == password) != null;

        }


        public void delete_admin(Admin _admin)
        {
            IserviceEvent spe = new ServiceEvent();
            int id = this.Get(x => x.isSuperAdmin==true).idAdmin;
            List<Event> eve = spe.GetMany(x => x.adminid == _admin.idAdmin).ToList();
            foreach(Event i in eve)
            {
                i.adminid = id;
                spe.Update(i);
                spe.Commit();
            }
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
                SHA256 hash = new SHA256CryptoServiceProvider();
                Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(_admin.passwordAdmin);
                Byte[] encodedBytes = hash.ComputeHash(originalBytes);
                _admin.passwordAdmin = BitConverter.ToString(encodedBytes);
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
