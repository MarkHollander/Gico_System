using System.Threading.Tasks;
using Gico.SystemAppService.Interfaces;
using Gico.SystemModels.Request;
using System;
using System.Linq;
using Gico.SystemDomains;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Mapping;
using Gico.SystemModels.Models;
using Gico.AppService;
using Gico.Config;
namespace Gico.SystemAppService.Implements
{
    public class ManufacturerCategoryMappingAppService : IManufacturerCategoryMappingAppService
    {

        private readonly ICurrentContext _context;
    
        private readonly ILogger<ManufacturerCategoryMappingAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly IManufacturerCategoryMappingService _manufacturerCategoryMappingService;

        public ManufacturerCategoryMappingAppService(ILogger<ManufacturerCategoryMappingAppService> logger, IManufacturerCategoryMappingService manufacturerCategoryMappingService, ICurrentContext context, ICommonService commonService)
        {
            _logger = logger;
            _manufacturerCategoryMappingService = manufacturerCategoryMappingService;
            _context = context;
            _commonService = commonService;
           
        }

        public async Task<ManufacturerMappingAddResponse> Add(ManufacturerMappingAddRequest request)
        {
            ManufacturerMappingAddResponse response = new ManufacturerMappingAddResponse();
            try
            {

                var command = request.ToAddCommand();

                var result = await _manufacturerCategoryMappingService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }


            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ManufacturerMapping_GetManufacturerResponse> Gets(ManufacturerMapping_GetManufacturerRequest request)
        {
            ManufacturerMapping_GetManufacturerResponse response = new ManufacturerMapping_GetManufacturerResponse();
            try
            {

                var manufacturers = await _manufacturerCategoryMappingService.Gets(request.CategoryId);
                if (manufacturers == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                    return response;
                }
                response.Manufacturers = manufacturers?.Select(p => p.ToModel()).ToArray();


            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;


        }

        public async Task<ManufacturerCategoryMappingRemoveResponse> ManufacturerCategoryMappingRemove(ManufacturerCategoryMappingRemoveRequest request)
        {
            ManufacturerCategoryMappingRemoveResponse response = new ManufacturerCategoryMappingRemoveResponse();
            try
            {
                var manufacturerCategory = new ManufacturerCategoryMappingModel
                {
                    CategoryId = request.CategoryId,
                    ManufacturerId = request.ManufacturerId
                };

                var command = manufacturerCategory.ToRemoveCommand();
                var result = await _manufacturerCategoryMappingService.SendCommand(command);
                if (result.IsSucess)
                {
                    response.SetSucess();
                }
                else
                {
                    response.SetFail(result.Message);
                }


            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }
    }
}
