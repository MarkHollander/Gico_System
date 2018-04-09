using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.SystemService.Implements
{
    public class MeasureUnitService : IMeasureUnitService
    {
        private readonly ICommandSender _commandService;
        private readonly IMeasureUnitRepository _measureUnitRepository;

        public MeasureUnitService(ICommandSender commandService, IMeasureUnitRepository measureUnitRepository)
        {
            _commandService = commandService;
            _measureUnitRepository = measureUnitRepository;

        }

        #region READ

        public async Task<RMeasureUnit[]> Search(string unitName, EnumDefine.GiftCodeCampaignStatus unitStatus, RefSqlPaging sqlPaging)
        {
            return await _measureUnitRepository.Search(unitName, unitStatus, sqlPaging);
        }

        public async Task<RMeasureUnit> GetById(string id)
        {
            return await _measureUnitRepository.GetById(id);
        }

        #endregion

        #region WRITE

        public async Task AddToDb(MeasureUnit measure)
        {
            await _measureUnitRepository.Add(measure);
        }

        public async Task ChangeToDb(MeasureUnit measure)
        {
            await _measureUnitRepository.Change(measure);
        }

        #endregion

        #region COMMAND

        public async Task<CommandResult> SendCommand(MeasureUnitAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(MeasureUnitChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        #endregion
    }


}
