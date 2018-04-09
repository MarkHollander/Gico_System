using System;
using System.Threading.Tasks;
using Gico.ReadSystemModels;
using Gico.SystemEvents.Cache;

namespace Gico.SystemCacheStorage.Interfaces
{
    public interface ILanguageCacheStorage
    {
        Task<RLanguage[]> Get();
        Task<RLanguage> Get(string id);
        Task<bool> AddOrChange(RLanguage currency);
        Task<bool> Remove(string id);

    }
}