using System;
using System.Threading.Tasks;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.SystemCommands;
using Gico.SystemDomains;
using Gico.SystemService.Interfaces;

namespace Gico.SystemCommandsHandler
{
    public class ProductAttributeCommandHandler : ICommandHandler<ProductAttributeCommand, ICommandResult>
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly IEventSender _eventSender;

        public ProductAttributeCommandHandler(IProductAttributeService productAttributeService, IEventSender eventSender)
        {
            _productAttributeService = productAttributeService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(ProductAttributeCommand mesage)
        {
            try
            {
                var item = new ProductAttribute();
                item.Init(mesage);

                if (!string.IsNullOrEmpty(mesage.Id))
                {
                    await _productAttributeService.UpdateToDb(item);
                    await _eventSender.Notify(item.Events);
                }
                else
                {
                    await _productAttributeService.AddToDb(item);
                    await _eventSender.Notify(item.Events);
                }

                var result = new CommandResult
                {
                    Message = "",
                    ObjectId = item.Id,
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

    public class ProductAttributeValueCommandHandler : ICommandHandler<ProductAttributeValueCommand, ICommandResult>
    {
        private readonly IProductAttributeValueService _productAttributeValueService;
        private readonly IEventSender _eventSender;

        public ProductAttributeValueCommandHandler(IProductAttributeValueService productAttributeValueService, IEventSender eventSender)
        {
            _productAttributeValueService = productAttributeValueService;
            _eventSender = eventSender;
        }

        public async Task<ICommandResult> Handle(ProductAttributeValueCommand mesage)
        {
            try
            {
                var item = new ProductAttributeValue();
                item.Init(mesage);

                if (!string.IsNullOrEmpty(mesage.AttributeValueId))
                {
                    await _productAttributeValueService.UpdateToDb(item);
                    await _eventSender.Notify(item.Events);
                }
                else
                {
                    await _productAttributeValueService.AddToDb(item);
                    await _eventSender.Notify(item.Events);
                }

                var result = new CommandResult
                {
                    Message = "",
                    ObjectId = item.Id,
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