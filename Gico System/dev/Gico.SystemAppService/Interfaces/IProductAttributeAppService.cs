using System.Threading.Tasks;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Interfaces
{
    public interface IProductAttributeAppService
    {
        Task<ProductAttributeSearchResponse> Search(ProductAttributeSearchRequest request);
        Task<ProductAttributeGetResponse> Get(ProductAttributeSearchRequest request);
        Task<ProductAttributeCrudResponse> AddOrUpdate(ProductAttributeCrudRequest request);
    }

    public interface IProductAttributeValueAppService
    {
        Task<ProductAttributeValueSearchResponse> Search(ProductAttributeValueSearchRequest request);
        Task<ProductAttributeValueGetResponse> Get(ProductAttributeValueSearchRequest request);
        Task<ProductAttributeValueCrudResponse> AddOrUpdate(ProductAttributeValueCrudRequest request);
    }
}