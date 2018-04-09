using System;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.SystemEvents.Cache;

namespace Gico.SystemCacheStorage.Interfaces
{
    public interface ICurrencyCacheStorage
    {
        Task<RCurrency[]> Get();
        Task<RCurrency> Get(string id);
        Task AddOrChange(RCurrency currency);
        Task Remove(string id);

    }
}