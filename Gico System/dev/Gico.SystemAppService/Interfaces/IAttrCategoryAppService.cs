using System.Threading.Tasks;
using Gico.SystemModels.Response;
using Gico.SystemModels.Request;

namespace Gico.SystemAppService.Interfaces
{
    public interface IAttrCategoryAppService
    {
        Task<AttrCategoryAddOrChangeResponse> AttrCategoryAdd(AttrCategoryAddRequest request);
        Task<AttrCategoryGetResponse> AttrCategoryGet(AttrCategoryGetRequest request);
        Task<AttrCategoryAddOrChangeResponse> AttrCategoryChange(AttrCategoryChangeRequest request);

        Task<AttrCategoryRemoveResponse> AttrCategoryRemove(AttrCategoryRemoveRequest request);

        Task<AttrCategoryMapping_GetsProductAttrResponse> GetsProductAttr(AttrCategoryMapping_GetsProductAttrRequest request);
    }
}
