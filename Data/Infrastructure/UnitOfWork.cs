using Infrastructure;
using MyFinance.Data.Infrastructure;


namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
       
         private DatabContext dataContext;

        IDatabaseFactory dbFactory;
        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            dataContext = dbFactory.DataContext;
        }
        
        public void Commit()
        {
            dataContext.SaveChanges();
        }
         public void CommitAsync()
         {
             dataContext.SaveChangesAsync();
         }
        public void Dispose()
        {
            dataContext.Dispose();
        }

        public IRepositoryBase<T> getRepository<T>() where T : class
        {
            IRepositoryBase<T> repo = new RepositoryBase<T>(dbFactory);
            return repo;

        }
    }
}
