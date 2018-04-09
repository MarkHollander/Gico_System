using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemCommands.Product;
using Gico.SystemService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gico.SystemCommandsHandler.Product
{
    public class ProductCommandHandler : ICommandHandler<ProductAddCommand, ICommandResult>,
        ICommandHandler<ProductChangeCommand, ICommandResult>,
        ICommandHandler<ProductCategoryMappingAddCommand, ICommandResult>,
        ICommandHandler<ProductCategoryMappingChangeCommand, ICommandResult>,
        ICommandHandler<ProductManufacturerMappingAddCommand, ICommandResult>,
        ICommandHandler<ProductManufacturerMappingChangeCommand, ICommandResult>,
        ICommandHandler<ProductProductAttributeMappingAddCommand, ICommandResult>,
        ICommandHandler<ProductProductAttributeMappingChangeCommand, ICommandResult>,
        ICommandHandler<VendorProductMappingAddCommand, ICommandResult>,
        ICommandHandler<VendorProductMappingChangeCommand, ICommandResult>,
        ICommandHandler<WarehouseProductMappingAddCommand, ICommandResult>,
        ICommandHandler<WarehouseProductMappingChangeCommand, ICommandResult>
    {        
        private readonly ICommonService _commonService;
        private readonly IEventSender _eventSender;
        public ProductCommandHandler(ICommonService commonService, IEventSender eventSender)
        {            
            _commonService = commonService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(ProductAddCommand mesage)
        {
            try
            {                
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductChangeCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductCategoryMappingAddCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductCategoryMappingChangeCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductManufacturerMappingAddCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductManufacturerMappingChangeCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductProductAttributeMappingAddCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(ProductProductAttributeMappingChangeCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(VendorProductMappingAddCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(VendorProductMappingChangeCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(WarehouseProductMappingAddCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
        public async Task<ICommandResult> Handle(WarehouseProductMappingChangeCommand mesage)
        {
            try
            {
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    //ObjectId = banner.Id,
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
