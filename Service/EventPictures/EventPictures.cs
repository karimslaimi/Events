using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;


namespace Service
{
    public  class EventPictures:servicePattern<EventPicture>,IEventPictures
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public EventPictures() : base(uow)
        {

        }

    }
}
