using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemDataObject.Interfaces;
using Gico.SystemDomains;
using Gico.SystemService.Implements;
using Gico.SystemService.Interfaces;
using System.Linq;

namespace Gico.SystemCommandsHandler
{
    public class ManufacturerCommandHandler: ICommandHandler<ManufacturerManagementAddCommand, ICommandResult>, ICommandHandler<ManufacturerManagementChangeCommand, ICommandResult>
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly ICommonService _commonService;
        public ManufacturerCommandHandler(IManufacturerService manufacturerService, ICommonService commonService)
        {
            _manufacturerService = manufacturerService;
            _commonService = commonService;
        }

        public async Task<ICommandResult> Handle(ManufacturerManagementAddCommand mesage)
        {
            try
            {
                Manufacturer manufacturer = new Manufacturer();
                manufacturer.Add(mesage);
                await _manufacturerService.AddToDb(manufacturer);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = Convert.ToString(manufacturer.Id),
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
                ICommandResult result = new CommandResult()
                {
                    Message = e.Message,
                    Status = CommandResult.StatusEnum.Fail
                };
                return result;
            }
        }

        public async Task<ICommandResult> Handle(ManufacturerManagementChangeCommand mesage)
        {
            try
            {
                Manufacturer manufacturer = new Manufacturer(mesage.Version);                
                manufacturer.Change(mesage);
                await _manufacturerService.ChangeToDb(manufacturer);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = Convert.ToString(manufacturer.Id),
                    Status = CommandResult.StatusEnum.Sucess
                };
                return result;
            }
            catch (Exception e)
            {
                e.Data["Param"] = mesage;
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
