using System;
using System.Collections.Generic;
using System.Text;
using Gico.SystemService.Interfaces;
using Gico.ReadSystemModels;
using Gico.SystemModels.Request;
using System.Threading.Tasks;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemDataObject.Interfaces;
using Gico.Config;
using Gico.SystemCommands;
using Gico.CQRS.Model.Implements;
using Gico.SystemDomains;
using Gico.ExceptionDefine;



namespace Gico.SystemService.Implements
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICommandSender _commandService;

        public ManufacturerService(IManufacturerRepository manufacturerRepository, ICommandSender commandService)
        {
            _manufacturerRepository = manufacturerRepository;
            _commandService = commandService;
        }
        public async Task<RManufacturer[]> GetAll(ManufacturerGetRequest request)
        {
            return await _manufacturerRepository.GetAll();
        }
        public async Task<RManufacturer[]> Search(ManufacturerGetRequest request)
        {
            RefSqlPaging p = new RefSqlPaging(request.PageIndex, request.PageSize);
            return await _manufacturerRepository.Search(request.Name, 0, p);
        }

        public async Task<RManufacturer[]> Search(string name, EnumDefine.StatusEnum status, RefSqlPaging sqlPaging)
        {
            return await _manufacturerRepository.Search(name, status, sqlPaging);
        }

        public async Task<RManufacturer> GetById(string v2)
        {

            return await _manufacturerRepository.GetById(v2);
        }

        public async Task<RManufacturer[]> GetById(string[] ids)
        {
            return await _manufacturerRepository.GetById(ids);
        }

        public async Task<CommandResult> SendCommand(ManufacturerManagementAddCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }
        public async Task<CommandResult> SendCommand(ManufacturerManagementChangeCommand command)
        {
            CommandResult commandResult = await _commandService.SendAndReceiveResult<CommandResult>(command);
            return commandResult;
        }

        public async Task AddToDb(Manufacturer manufacturer)
        {
            await _manufacturerRepository.Add(manufacturer);
        }
        public async Task ChangeToDb(Manufacturer manufacturer)
        {
            bool isChanged = await _manufacturerRepository.Change(manufacturer);
            if (!isChanged)
            {
                throw new MessageException(ResourceKey.Manufacturer_NotChanged);
            }
        }
    }
}
