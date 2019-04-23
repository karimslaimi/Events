using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;

namespace Service
{
    public class serviceNL:servicePattern<Subscribers>, IserviceNL
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceNL() : base(uow)
        {

        }
    }
}
