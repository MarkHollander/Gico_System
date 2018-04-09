using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDomains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.SystemService.Interfaces
{
    public interface IMeasureUnitService
    {
        #region READ From DB

        Task<RMeasureUnit[]> Search(string unitName,  EnumDefine.GiftCodeCampaignStatus unitStatus, RefSqlPaging sqlPagingg);
        Task<RMeasureUnit> GetById(string id);

        #endregion

        #region WRITE To DB

        Task AddToDb(MeasureUnit measure);
        Task ChangeToDb(MeasureUnit measure);

        #endregion

        #region COMMAND
        Task<CommandResult> SendCommand(MeasureUnitAddCommand command);
        Task<CommandResult> SendCommand(MeasureUnitChangeCommand command);


        #endregion
    }
}
