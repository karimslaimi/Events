using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service.EventFolder;
using System.Linq;

namespace Service
{
    public class serviceTheme:servicePattern<Theme>,IserviceTheme
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceTheme() : base(uow)
        {

        }

        public dynamic Themestat()
        {
            IserviceEvent spe = new ServiceEvent();
            var eve = spe.GetAll();

            var _event = eve.GroupBy(s => s.theme).Select(s => new { thname = s.Key.designation, thval = s.Count() }).ToList();
            return _event;
        }


    }
}
