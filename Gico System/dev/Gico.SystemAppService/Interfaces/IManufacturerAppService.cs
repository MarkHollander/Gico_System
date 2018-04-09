using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.SystemModels.Response;
using Gico.SystemModels.Request;


namespace Gico.SystemAppService.Interfaces
{
    public interface IManufacturerAppService
    {
        Task<ManufacturerGetResponse> GetAll(ManufacturerGetRequest request);
        Task<ManufacturerGetResponse> Search(ManufacturerGetRequest request);

        Task<ManufacturerGetResponse> GetById(ManufacturerGetRequest request);
        Task<ManufacturerGetResponse> AddOrChange(ManufacturerManagementAddOrChangeRequest request);
        
    }
}
