using System;
using System.Threading.Tasks;
using Gico.Config;
using Gico.CQRS.Model.Implements;
using Gico.CQRS.Model.Interfaces;
using Gico.CQRS.Service.Interfaces;
using Gico.ReadSystemModels.ProductGroup;
using Gico.SystemCommands.PageBuilder;
using Gico.SystemCommands.ProductGroup;
using Gico.SystemDomains.ProductGroup;
using Gico.SystemService.Interfaces.ProductGroup;

namespace Gico.SystemCommandsHandler.PageBuilder
{
    public class ProductGroupCommandHandler : ICommandHandler<ProductGroupAddCommand, ICommandResult>,
        ICommandHandler<ProductGroupChangeCommand, ICommandResult>,
        ICommandHandler<ProductGroupCategoryChangeCommand, ICommandResult>,
        ICommandHandler<ProductGroupVendorAddCommand, ICommandResult>,
        ICommandHandler<ProductGroupVendorRemoveCommand, ICommandResult>,
        ICommandHandler<ProductGroupAttributeAddCommand, ICommandResult>,
        ICommandHandler<ProductGroupAttributeChangeCommand, ICommandResult>,
        ICommandHandler<ProductGroupAttributeRemoveCommand, ICommandResult>,
        ICommandHandler<ProductGroupPriceChangeCommand, ICommandResult>,
        ICommandHandler<ProductGroupQuantityChangeCommand, ICommandResult>,
        ICommandHandler<ProductGroupManufacturAddCommand, ICommandResult>,
        ICommandHandler<ProductGroupManufacturRemoveCommand, ICommandResult>,
        ICommandHandler<ProductGroupWarehouseAddCommand, ICommandResult>,
        ICommandHandler<ProductGroupWarehouseRemoveCommand, ICommandResult>,
        ICommandHandler<ProductGroupProductAddCommand, ICommandResult>,
        ICommandHandler<ProductGroupProductRemoveCommand, ICommandResult>
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupCommandHandler(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        public async Task<ICommandResult> Handle(ProductGroupAddCommand message)
        {
            try
            {
                ProductGroup productGroup = new ProductGroup();
                productGroup.Add(message);
                await _productGroupService.Add(productGroup);
                ICommandResult result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.Id);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.Change(message);
                await _productGroupService.Change(productGroup);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupCategoryChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.ChangeCategory(message.CategoryIds, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupVendorAddCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.AddVendor(message.VendorId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupVendorRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.RemoveVendor(message.VendorId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupAttributeAddCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.AddAttribute(message);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupAttributeChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.ChangeAttribute(message);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupAttributeRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.RemoveAttribute(message);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupPriceChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.ChangePrice(message.Prices);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupQuantityChangeCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.ChangeQuantity(message.Quantities);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupManufacturAddCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.AddManufacturer(message.ManufacturerId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupManufacturRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.RemoveManufacturer(message.ManufacturerId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupWarehouseAddCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.AddWarehouse(message.WarehouseId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupWarehouseRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.RemoveWarehouse(message.WarehouseId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupProductAddCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.AddProduct(message.ProductId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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

        public async Task<ICommandResult> Handle(ProductGroupProductRemoveCommand message)
        {
            try
            {
                ICommandResult result;
                RProductGroup rproductGroup = await _productGroupService.Get(message.ProductGroupId);
                if (rproductGroup == null)
                {
                    result = new CommandResult()
                    {
                        Message = "ProductGroup not found",
                        ObjectId = "",
                        Status = CommandResult.StatusEnum.Fail,
                        ResourceName = ResourceKey.ProductGroup_NotFound
                    };
                    return result;
                }
                ProductGroup productGroup = new ProductGroup(rproductGroup);
                productGroup.RemoveProduct(message.ProductId, message.UpdatedUid, message.CreatedDateUtc);
                await _productGroupService.ChangeConditions(productGroup.Id, productGroup.Conditions, productGroup.UpdatedUid, productGroup.UpdatedDateUtc);
                result = new CommandResult()
                {
                    Message = "",
                    ObjectId = productGroup.Id,
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