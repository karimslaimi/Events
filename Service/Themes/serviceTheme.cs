using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;


namespace Service
{
    public class serviceTheme:servicePattern<Theme>,IserviceTheme
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceTheme() : base(uow)
        {

        }
    }
}
