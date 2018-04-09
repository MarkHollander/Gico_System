using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces.PageBuilder
{
    public interface IMenuAppService
    {
        Task<MenuGetsResponse> MenuGet(MenuGetsRequest request);
        Task<MenuGetResponse> MenuGet(MenuGetRequest request);
        Task<MenuAddOrChangeResponse> MenuAddOrChange(MenuAddOrChangeRequest request);
        Task<BannersGetByMenuIdResponse> BannerGetByMenuId(BannerGetByMenuIdRequest request);
        Task<BaseResponse> MenuBannerMappingAdd(MenuBannerMappingAddRequest request);
        Task<BaseResponse> MenuBannerMappingRemove(MenuBannerMappingRemoveRequest request);
    }
}