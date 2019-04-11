using System;
using Data.Infrastructure;

namespace MyFinance.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<T> getRepository<T>() where T : class; 
        void Commit();
       
    }

}
