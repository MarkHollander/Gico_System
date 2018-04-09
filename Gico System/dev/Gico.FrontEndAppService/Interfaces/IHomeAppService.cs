using System.Threading.Tasks;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEndAppService.Interfaces
{
    public interface IHomeAppService : IPageAppService
    {
        Task<HomeViewModel> Get(string tmpCode);
    }
}