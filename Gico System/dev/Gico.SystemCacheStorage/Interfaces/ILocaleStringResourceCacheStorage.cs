using System;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.SystemEvents.Cache;

namespace Gico.SystemCacheStorage.Interfaces
{
    public interface ILocaleStringResourceCacheStorage
    {
        Task<RLocaleStringResource[]> Get(string[] resourceNames, string languageId);
        Task<bool> AddOrChange(RLocaleStringResource currency);
        Task<bool> Remove(string id, string languageId);

    }
}