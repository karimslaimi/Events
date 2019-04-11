using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;


namespace Service
{
    public class serviceOrganization:servicePattern<organization>,IserviceOrganization
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceOrganization() : base(uow)
        {

        }

    }
}
