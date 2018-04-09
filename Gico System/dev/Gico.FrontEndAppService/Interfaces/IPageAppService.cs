using Gico.FrontEndModels;
using System.Threading.Tasks;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEndAppService.Interfaces
{
    public interface IPageAppService
    {
        Task<PageModel> InitPage();
        Task<AjaxModel> InitAjax();
        Task<bool> IsAuthenticated();
    }

}