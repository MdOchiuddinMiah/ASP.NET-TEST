using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppAuthentication.Interface
{
    public interface IBaseRepository : IDisposable
    {
        Task Save();
    }
}
