using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service.EventFolder;
using System.Collections.Generic;
using System.Linq;

namespace Service
{ 
   public class serviceUniv:servicePattern<University>,IserviceUniv
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceUniv() : base(uow)
        {

        }

        public dynamic Univstat()
        {

            IserviceEvent spe = new ServiceEvent();
            IserviceUniv spun = new serviceUniv();
            int total = spe.GetAll().Count();
            var univstat = spun.GetAll().Select(x =>new { name = x.UnivName, y = spe.GetMany(s => s.hostedby.idUniv == x.idUniv).Count() }).ToList();

            
            return univstat.Select(x => new { name = x.name, y = x.y });
        }
    }
}
