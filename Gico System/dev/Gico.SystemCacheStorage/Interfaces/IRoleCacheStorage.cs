using System.Threading.Tasks;
using Gico.ReadSystemModels;

namespace Gico.SystemCacheStorage.Interfaces
{
    public interface IRoleCacheStorage
    {
        Task<bool> ActionDefineAdd(RActionDefine actionDefine);
        Task<RActionDefine> ActionDefineGet(string id);
        Task<RActionDefine[]> ActionDefineGet(string[] ids);
        Task<bool> CheckExists(string id);
    }
}