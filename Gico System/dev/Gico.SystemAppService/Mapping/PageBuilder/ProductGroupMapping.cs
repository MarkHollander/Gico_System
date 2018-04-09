using System;
using System.Linq;
using Gico.ReadSystemModels.ProductGroup;
using Gico.SystemCommands.ProductGroup;
using Gico.SystemModels.Models.ProductGroup;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.SystemAppService.Mapping.PageBuilder
{
    public static class ProductGroupMapping
    {
        public static ProductGroupViewModel ToModel(this RProductGroup productGroup)
        {
            if (productGroup == null)
            {
                return null;
            }
            return new ProductGroupViewModel()
            {
                Status = productGroup.Status,
                Id = productGroup.Id,
                Name = productGroup.Name,
                //Conditions = productGroup.Conditions,
                Description = productGroup.Description,

            };
        }
        public static ProductGroupAddCommand ToCommandAdd(this ProductGroupAddOrChangeRequest productGroup, string uid)
        {
            if (productGroup == null)
            {
                return null;
            }
            return new ProductGroupAddCommand()
            {
                Status = productGroup.Status,
                Name = productGroup.Name,
                Description = productGroup.Description,
                CreatedUid = uid,
            };
        }
        public static ProductGroupChangeCommand ToCommandChange(this ProductGroupAddOrChangeRequest productGroup, string uid)
        {
            if (productGroup == null)
            {
                return null;
            }
            return new ProductGroupChangeCommand()
            {
                Status = productGroup.Status,
                Name = productGroup.Name,
                Description = productGroup.Description,
                CreatedUid = uid,
                Id = productGroup.Id
            };
        }
        public static ProductGroupCategoryChangeCommand ToCommandCategoryChange(this ProductGroupCategoryChangeRequest productGroup, string uid)
        {
            if (productGroup == null)
            {
                return null;
            }
            return new ProductGroupCategoryChangeCommand()
            {
                ProductGroupId = productGroup.ProductGroupId,
                CategoryIds = productGroup.CategoryIds,
                UpdatedUid = uid
            };
        }
        public static ProductGroupVendorAddCommand ToCommandVenderAdd(this ProductGroupVendorAddRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupVendorAddCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                VendorId = request.VendorId
            };
        }
        public static ProductGroupVendorRemoveCommand ToCommandVenderRemove(this ProductGroupVendorRemoveRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupVendorRemoveCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                VendorId = request.VendorId
            };
        }
        public static ProductGroupWarehouseAddCommand ToCommandWarehouseAdd(this ProductGroupWarehouseAddRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupWarehouseAddCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                WarehouseId = request.WarehouseId
            };
        }
        public static ProductGroupWarehouseRemoveCommand ToCommandWarehouseRemove(this ProductGroupWarehouseRemoveRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupWarehouseRemoveCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                WarehouseId = request.WarehouseId
            };
        }
        public static ProductGroupManufacturAddCommand ToCommandManufacturerAdd(this ProductGroupManufacturerAddRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupManufacturAddCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                ManufacturerId = request.ManufacturerId
            };
        }
        public static ProductGroupManufacturRemoveCommand ToCommandManufacturerRemove(this ProductGroupManufacturerRemoveRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupManufacturRemoveCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                ManufacturerId = request.ManufacturerId
            };
        }
        public static ProductGroupAttributeAddCommand ToCommandAttributeAdd(this ProductGroupAddOrChangeAttributeRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupAttributeAddCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                AttributeId = request.AttributeId,
                AttributeValueIds = request.AttributeValueIds
            };
        }
        public static ProductGroupAttributeChangeCommand ToCommandAttributeChange(this ProductGroupAddOrChangeAttributeRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupAttributeChangeCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                AttributeId = request.AttributeId,
                AttributeValueIds = request.AttributeValueIds
            };
        }
        public static ProductGroupAttributeRemoveCommand ToCommandAttributeRemove(this ProductGroupRemoveAttributeRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupAttributeRemoveCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                AttributeId = request.AttributeId,
            };
        }
        public static ProductGroupPriceChangeCommand ToCommandPriceChange(this ProductGroupPriceConfigChangeRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            if (request.Prices == null)
            {
                request.Prices = new ProductGroupPriceConfigModel[0];
            }
            var prices = new Decimal[request.Prices.Length * 2];
            for (int i = 0; i < request.Prices.Length; i++)
            {
                prices[i * 2] = request.Prices[i].MinPrice;
                prices[(i * 2) + 1] = request.Prices[i].MaxPrice;
            }
            return new ProductGroupPriceChangeCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                Prices = prices
            };

        }
        public static ProductGroupQuantityChangeCommand ToCommandQuantityChange(this ProductGroupQuantityConfigChangeRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            if (request.Quantities == null)
            {
                request.Quantities = new ProductGroupQuantityConfigModel[0];
            }
            var quantities = new int[request.Quantities.Length * 2];
            for (int i = 0; i < request.Quantities.Length; i++)
            {
                quantities[i * 2] = request.Quantities[i].MinQuantity;
                quantities[(i * 2) + 1] = request.Quantities[i].MaxQuantity;
            }
            return new ProductGroupQuantityChangeCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                Quantities = quantities
            };

        }
        public static ProductGroupProductAddCommand ToCommandProductAdd(this ProductGroupProductAddRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupProductAddCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                ProductId = request.ProductId
            };
        }
        public static ProductGroupProductRemoveCommand ToCommandProductRemove(this ProductGroupProductRemoveRequest request, string uid)
        {
            if (request == null)
            {
                return null;
            }
            return new ProductGroupProductRemoveCommand()
            {
                ProductGroupId = request.ProductGroupId,
                UpdatedUid = uid,
                ProductId = request.ProductId
            };
        }
    }
}
