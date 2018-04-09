using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gico.Config;
using Gico.SystemModels.Response;
using Gico.SystemModels.Request;
using Gico.ReadSystemModels;
using Gico.SystemDomains;
using Gico.SystemCommands;
using Gico.CQRS.Model.Implements;

namespace Gico.SystemService.Interfaces
{
    public interface IManufacturerService
    {
        Task<RManufacturer[]> GetAll(ManufacturerGetRequest request);
        Task<RManufacturer[]> Search(ManufacturerGetRequest request);
        Task<RManufacturer[]> Search(string name, EnumDefine.StatusEnum status, RefSqlPaging sqlPaging);

        Task<RManufacturer> GetById(string id);
        Task<RManufacturer[]> GetById(string[] ids);

        Task AddToDb(Manufacturer manufacturer);
        Task ChangeToDb(Manufacturer manufacturer);

        Task<CommandResult> SendCommand(ManufacturerManagementAddCommand command);
        Task<CommandResult> SendCommand(ManufacturerManagementChangeCommand command);
    }
}
