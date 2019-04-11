
using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;

namespace Service
{
    public class serviceUser : servicePattern<User>, IserviceUser
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceUser() : base(uow)
        {

        }

      

        public bool AuthUser(string username, string password)
        {
           
            return this.Get(x => x.username == username && x.password == password)!=null;
           
        }



        public void edit_user_profile(User _user)
        {
            User us = this.GetById(_user.id);
            if (!string.IsNullOrEmpty(_user.lastname) && !string.IsNullOrWhiteSpace(_user.lastname) )
            {
                us.lastname = _user.lastname;
            }
            if (!string.IsNullOrEmpty(_user.firstname) && !string.IsNullOrWhiteSpace(_user.firstname))
            {
                us.firstname = _user.firstname;
            }
            if (!string.IsNullOrEmpty(_user.mail) && !string.IsNullOrWhiteSpace(_user.mail))
            {
                us.mail = _user.mail;
            }
            if (!string.IsNullOrEmpty(_user.phone) && !string.IsNullOrWhiteSpace(_user.phone) )
            {
                us.phone = _user.phone;
            }
            if(!string.IsNullOrEmpty(_user.password) && !string.IsNullOrWhiteSpace(_user.password))
            {
                us.password = _user.password;
            }
            this.Update(us);
            this.Commit();
        }

        public void register_user(User _user)
        {

            this.Add(_user);
            this.Commit();
        }
    }
}
