using System.Threading.Tasks;
using Gico.Config;
using Gico.ReadSystemModels;
using Gico.SystemDomains;

namespace Gico.SystemDataObject.Interfaces
{
    public interface IActionDefineRepository
    {
        Task<RActionDefine> Get(string id);
        Task<RActionDefine[]> Get(string[] ids);
        Task<RActionDefine[]> Get(string group, string name, RefSqlPaging paging);
        Task Add(ActionDefine actionDefine);
    }
}