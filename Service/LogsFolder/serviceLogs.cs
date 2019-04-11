using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;

namespace Service
{
    public class serviceLogs:servicePattern<Logs>,IserviceLogs
    {

        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceLogs() : base(uow)
        {

        }

    }
}
