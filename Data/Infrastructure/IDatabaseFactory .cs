using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DatabContext DataContext { get; }
    }

}
