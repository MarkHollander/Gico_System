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
using Gico.CQRS.Model.Implements;

namespace Gico.SystemAppService.Implements
{
    public class ManufacturerAppService: IManufacturerAppService
    {
        private readonly ICurrentContext _context;
        private readonly ILogger<ManufacturerAppService> _logger;
        private readonly ICommonService _commonService;
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerAppService(ILogger<ManufacturerAppService> logger, IManufacturerService manufacturerService, ICurrentContext context, ICommonService commonService)
        {
            _logger = logger;
            _manufacturerService = manufacturerService;
            _context = context;
            _commonService = commonService;
        }

        public async Task<ManufacturerGetResponse> GetAll (ManufacturerGetRequest request)
        {
            ManufacturerGetResponse response = new ManufacturerGetResponse();
            try
            {
                var manufacturers = await _manufacturerService.GetAll(request);
                if (manufacturers == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                    return response;
                }
                response.Manufacturers = manufacturers.Select(p => p.ToModel()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ManufacturerGetResponse> GetById(ManufacturerGetRequest request)
        {
            ManufacturerGetResponse response = new ManufacturerGetResponse();
            try
            {

                
                var manufacturer = await _manufacturerService.GetById(request.Id);
                if (manufacturer == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                    return response;
                }
                response.Manufacturers = new ManufacturerViewModel[1];
                response.Manufacturers[0] = manufacturer.ToModel();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ManufacturerGetResponse> Search(ManufacturerGetRequest request)
        {
            ManufacturerGetResponse response = new ManufacturerGetResponse();
            try
            {
                var manufacturers = await _manufacturerService.Search(request);
                if (manufacturers == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                    return response;
                }
                response.Manufacturers = manufacturers.Select(p => p.ToModel()).ToArray();
            }
            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<ManufacturerGetResponse> AddOrChange(ManufacturerManagementAddOrChangeRequest request)
        {
            ManufacturerGetResponse response = new ManufacturerGetResponse();

            try
            {                
                CommandResult result;
                if (request.Id == 0)
                {
                    var command = request.ToCommand_Add();
                    result = await _manufacturerService.SendCommand(command);
                }
                else
                {
                    var command = request.ToCommand_Change();
                    result = await _manufacturerService.SendCommand(command);
                }               
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

        public async Task<ManufacturerGetResponse> Change(ManufacturerGetRequest request)
        {
            ManufacturerGetResponse response = new ManufacturerGetResponse();
            try
            {
                var manufacturers = await _manufacturerService.GetAll(request);
                if (manufacturers == null)
                {
                    response.SetFail(BaseResponse.ErrorCodeEnum.UserNotFound);
                    return response;
                }
                response.Manufacturers = manufacturers.Select(p => p.ToModel()).ToArray();
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
