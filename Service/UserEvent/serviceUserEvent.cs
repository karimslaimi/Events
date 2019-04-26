

using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;

namespace Service
{
  public  class serviceUserEvent:servicePattern<UserEvent>,IServiceUserEvent
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceUserEvent() : base(uow)
        {

        }

        public void participate(int idu, int ide)
        {
            UserEvent uev = new UserEvent();
            uev.eventid = ide;
            uev.userid = idu;

            this.Add(uev);
            this.Commit();
        }

    }
}
