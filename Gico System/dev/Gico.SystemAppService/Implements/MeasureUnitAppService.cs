using Gico.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemAppService.Interfaces;
using Gico.SystemAppService.Mapping;
using Gico.SystemDomains;
using Gico.SystemModels.Request;
using Gico.SystemModels.Response;
using Gico.SystemService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gico.SystemAppService.Implements
{
    public class MeasureUnitAppService : IMeasureUnitAppService
    {
        private readonly IMeasureUnitService _measureUnitService;
        private readonly ICurrentContext _context;
        private readonly ICommonService _commonService;
        private readonly ILogger<MeasureUnitAppService> _logger;

        public MeasureUnitAppService(IMeasureUnitService measureUnitService, ICurrentContext context, ICommonService commonService, ILogger<MeasureUnitAppService> logger)
        {
            _measureUnitService = measureUnitService;
            _context = context;
            _commonService = commonService;
            _logger = logger;
        }

        public async Task<MeasureUnitSearchResponse> Search(MeasureUnitSearchRequest request)
        {
            MeasureUnitSearchResponse response = new MeasureUnitSearchResponse();

            RefSqlPaging paging = new RefSqlPaging(request.PageIndex, 30);
            try
            {
                RefSqlPaging sqlpaging = new RefSqlPaging(request.PageIndex, request.PageSize);
                RMeasureUnit[] measureUnit = await _measureUnitService.Search(request.UnitName, request.UnitStatus, sqlpaging);
                response.TotalRow = paging.TotalRow;
                response.MeasureUnits = measureUnit.Select(p => p.ToModel()).ToArray();
                response.BaseUnits = measureUnit.Select(p => p.ToKeyValueTypeStringModel()).ToArray();
                response.PageIndex = request.PageIndex;
                response.PageSize = request.PageSize;

                response.SetSucess();
            }


            catch (Exception e)
            {
                response.SetFail(e);
                _logger.LogError(e, e.Message, request);
            }
            return response;
        }

        public async Task<BaseResponse> Add(MeasureUnitAddRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                var command = request.ToCommand(userLogin.Id);
                CommandResult result = await _measureUnitService.SendCommand(command);
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

        public async Task<BaseResponse> Change(MeasureUnitChangeRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userLogin = await _context.GetCurrentCustomer();
                var command = request.ToCommand(userLogin.Id);
                CommandResult result = await _measureUnitService.SendCommand(command);
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
