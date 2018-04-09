using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ExceptionDefine;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;

namespace Gico.SystemCommandsHandler
{
    public class MeasureUnitCommandHandler : ICommandHandler<MeasureUnitAddCommand, ICommandResult>, ICommandHandler<MeasureUnitChangeCommand, ICommandResult>
    {
        private readonly IMeasureUnitService _measureUnitService;
        private readonly ICommonService _commonService;

        public MeasureUnitCommandHandler(IMeasureUnitService measureUnitService, ICommonService commonService)
        {
            _measureUnitService = measureUnitService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(MeasureUnitAddCommand message)
        {
            try
            {
                MeasureUnit measure = new MeasureUnit();
                measure.Add(message);
                await _measureUnitService.AddToDb(measure);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = measure.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }


        public async Task<ICommandResult> Handle(MeasureUnitChangeCommand message)
        {
            try
            {
                var measureUnitFromDb = await _measureUnitService.GetById(message.UnitId);
                if (measureUnitFromDb == null)
                {
                    throw new MessageException(ResourceKey.LocaleStringResource_NotFound);
                }
                MeasureUnit measure = new MeasureUnit(measureUnitFromDb);
                measure.Change(message);

                await _measureUnitService.ChangeToDb(measure);


                ICommandResult result = new CommandResult
                {
                    Message = "",
                    ObjectId = measure.Id,
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = message;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }
    }
}
