using System.Threading.Tasks;
using GaBon.SystemModels.Request;
using Gico.SystemModels;
using Gico.SystemModels.Request;
using Gico.SystemModels.Request.Warehouse;
using Gico.SystemModels.Response;
using Gico.SystemModels.Response.Warehouse;

namespace Gico.SystemAppService.Interfaces.Warehouse
{
    public interface IWarehouseAppService
    {
       Task<WarehouseSearchResponse> Search(WarehouseSearchRequest request);
       Task<WarehouseGetResponse> Get(WarehouseGetRequest request);
    }
}
