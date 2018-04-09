using System.Linq;
using Gico.Common;
using Gico.Models.Response;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemModels.Request;
using Gico.SystemModels.Request.ProductGroup;
using Gico.SystemModels.Response;

namespace Gico.SystemAppService.Mapping
{
    public static class ProductAttributeMapping
    {
        public static ProductAttributeViewModel ToModel(this RProductAttribute request)
        {
            if (request == null) return null;
            return new ProductAttributeViewModel()
            {
                AttributeId = request.AttributeId,
                AttributeName = request.AttributeName,
                AttributeStatus = request.AttributeStatus,
                CreatedOnUtc = request.CreatedOnUtc,
                CreatedUserId = request.CreatedUserId,
                UpdatedOnUtc = request.UpdatedOnUtc,
                UpdatedUserId = request.UpdatedUserId
            };
        }
        public static ProductAttributeViewModel ToModel(this RProductAttribute request, RProductAttributeValue[] attributeValues)
        {
            if (request == null) return null;
            var response = request.ToModel();
            response.AttributeValues = attributeValues?.Select(p => p.ToModel()).ToArray();
            return response;
        }

        public static ProductAttributeCommand ToCommand(this ProductAttributeCrudRequest request, string userId)
        {
            if (request == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(request.Id))
            {
                return new ProductAttributeCommand()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Status = request.Status,
                    UpdatedOnUtc = Extensions.GetCurrentDateUtc(),
                    UpdatedUserId = userId
                };
            }
            else
            {
                return new ProductAttributeCommand()
                {
                    Name = request.Name,
                    Status = request.Status,
                    CreatedOnUtc = Extensions.GetCurrentDateUtc(),
                    CreatedUserId = userId
                };
            }
        }

        public static KeyValueTypeStringModel ToKeyValueModel(this RProductAttribute attribute)
        {
            if (attribute == null)
            {
                return null;
            }
            return new KeyValueTypeStringModel()
            {
                Value = attribute.AttributeId,
                Text = attribute.AttributeName,
                Checked = false
            };
        }

        public static ProductGroupPriceConfigModel[] ToModel(this decimal[] prices)
        {
            if (prices == null || prices.Length <= 0) return new ProductGroupPriceConfigModel[0];
            ProductGroupPriceConfigModel[] models = new ProductGroupPriceConfigModel[prices.Length / 2];
            for (int i = 0; i < models.Length; i++)
            {
                models[i] = new ProductGroupPriceConfigModel()
                {
                    MinPrice = prices[i * 2],
                    MaxPrice = prices[(i * 2) + 1],
                };
            }
            return models;
        }
        public static ProductGroupQuantityConfigModel[] ToModel(this int[] quantities)
        {
            if (quantities == null || quantities.Length <= 0) return new ProductGroupQuantityConfigModel[0];
            ProductGroupQuantityConfigModel[] models = new ProductGroupQuantityConfigModel[quantities.Length / 2];
            for (int i = 0; i < models.Length; i++)
            {
                models[i] = new ProductGroupQuantityConfigModel()
                {
                    MinQuantity = quantities[i * 2],
                    MaxQuantity = quantities[(i * 2) + 1],
                };
            }
            return models;
        }

    }

    public static class ProductAttributeValueMapping
    {
        public static ProductAttributeValueViewModel ToModel(this RProductAttributeValue request)
        {
            if (request == null) return null;
            return new ProductAttributeValueViewModel()
            {
                AttributeValueId = request.AttributeValueId,
                AttributeId = request.AttributeId,
                Value = request.Value,
                UnitId = request.UnitId,
                AttributeValueStatus = request.AttributeValueStatus,
                DisplayOrder = request.DisplayOrder,
                CreatedUserId = request.CreatedUserId,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedUserId = request.UpdatedUserId,
                UpdatedOnUtc = request.CreatedOnUtc
            };
        }

        public static ProductAttributeValueCommand ToCommand(this ProductAttributeValueCrudRequest request, string userId)
        {
            if (request == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(request.Id))
            {
                return new ProductAttributeValueCommand()
                {
                    AttributeValueId = request.Id,
                    AttributeId = request.AttributeId,
                    Value = request.Value,
                    AttributeValueStatus = request.Status,
                    UnitId = request.UnitId,
                    DisplayOrder = request.Order,
                    UpdatedOnUtc = Extensions.GetCurrentDateUtc(),
                    UpdatedUserId = userId
                };
            }
            else
            {
                return new ProductAttributeValueCommand()
                {
                    AttributeId = request.AttributeId,
                    Value = request.Value,
                    AttributeValueStatus = request.Status,
                    UnitId = request.UnitId,
                    DisplayOrder = request.Order,
                    CreatedOnUtc = Extensions.GetCurrentDateUtc(),
                    CreatedUserId = userId
                };
            }
        }
        public static KeyValueTypeStringModel ToKeyValueModel(this RProductAttributeValue attributeValue)
        {
            if (attributeValue == null)
            {
                return null;
            }
            return new KeyValueTypeStringModel()
            {
                Value = attributeValue.AttributeValueId,
                Text = attributeValue.Value,
                Checked = false
            };
        }
    }
}
