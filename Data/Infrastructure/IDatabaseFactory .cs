using Data;
using System;


namespace Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DatabContext DataContext { get; }
    }

}
