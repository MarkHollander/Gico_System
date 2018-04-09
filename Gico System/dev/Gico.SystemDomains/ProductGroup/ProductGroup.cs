using Gico.Config;
using Gico.Domains;
using Gico.SystemCommands.ProductGroup;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gico.ReadSystemModels.ProductGroup;

namespace Gico.SystemDomains.ProductGroup
{
    public class ProductGroup : BaseDomain
    {
        public ProductGroup()
        {
            if (Conditions == null)
            {
                Conditions = new ConcurrentDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition>();
            }
        }

        public ProductGroup(RProductGroup productGroup)
        {
            Id = productGroup.Id;
            Name = productGroup.Name;
            Status = productGroup.Status;
            Description = productGroup.Description;
            Conditions = Common.Serialize.JsonDeserializeObject<IDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition>>(productGroup.Conditions);
            //var conditions = Common.Serialize.JsonDeserializeObject<IDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition>>(productGroup.Conditions);
            //if (conditions != null && conditions.Count > 0)
            //{
            //    foreach (var productGroupCondition in conditions)
            //    {
            //        Conditions.Add(productGroupCondition.Key,);
            //    }
            //}
            UpdatedDateUtc = productGroup.UpdatedDateUtc;
            CreatedDateUtc = productGroup.CreatedDateUtc;
            CreatedUid = productGroup.CreatedUid;
            UpdatedUid = productGroup.UpdatedUid;
        }

        #region Publish method      

        public void Add(ProductGroupAddCommand command)
        {
            Id = Common.Common.GenerateGuid();
            Name = command.Name ?? string.Empty;
            Description = command.Description ?? string.Empty;
            Status = command.Status;
            CreatedDateUtc = command.CreatedDateUtc;
            UpdatedDateUtc = command.CreatedDateUtc;
            CreatedUid = command.CreatedUid ?? string.Empty;
            UpdatedUid = command.CreatedUid ?? string.Empty;
        }

        public void Change(ProductGroupChangeCommand command)
        {
            Add(command);
            Id = command.Id;
            UpdatedDateUtc = command.UpdatedDateUtc;
            UpdatedUid = command.UpdatedUid ?? string.Empty;
        }

        private void InitConditions()
        {
            if (Conditions == null)
            {
                Conditions = new ConcurrentDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition>();
            }
        }

        public void ChangeCategory(string[] ids, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            Conditions.Remove(EnumDefine.ProductGroupConfigTypeEnum.Category);
            if (ids?.Length <= 0)
            {
                return;
            }
            Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Category, new ProductGroupCondition(EnumDefine.ProductGroupConfigTypeEnum.Category, new ProductGroupCategoryCondition(ids)));
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void AddManufacturer(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            EnumDefine.ProductGroupConfigTypeEnum key = EnumDefine.ProductGroupConfigTypeEnum.Manufacturer;
            if (!Conditions.ContainsKey(key))
            {
                Conditions.Add(key, new ProductGroupCondition(key, new ProductGroupVendorCondition()
                ));
            }
            ProductGroupManufacturerCondition condition = (ProductGroupManufacturerCondition)Conditions[key].Config;
            condition.Add(id);
            Conditions[key].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void RemoveManufacturer(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            EnumDefine.ProductGroupConfigTypeEnum key = EnumDefine.ProductGroupConfigTypeEnum.Manufacturer;
            if (!Conditions.ContainsKey(key))
            {
                Conditions.Add(key, new ProductGroupCondition(key, new ProductGroupVendorCondition()));
            }
            ProductGroupManufacturerCondition condition = (ProductGroupManufacturerCondition)Conditions[key].Config;
            condition.Remove(id);
            Conditions[key].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void AddVendor(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            if (!Conditions.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Vendor))
            {
                Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Vendor, new ProductGroupCondition(
                    EnumDefine.ProductGroupConfigTypeEnum.Vendor,
                    new ProductGroupVendorCondition()
                    ));
            }
            ProductGroupVendorCondition condition = (ProductGroupVendorCondition)Conditions[EnumDefine.ProductGroupConfigTypeEnum.Vendor].Config;
            condition.Add(id);
            Conditions[EnumDefine.ProductGroupConfigTypeEnum.Vendor].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void RemoveVendor(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            if (!Conditions.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Vendor))
            {
                Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Vendor, new ProductGroupCondition(
                    EnumDefine.ProductGroupConfigTypeEnum.Vendor,
                    new ProductGroupVendorCondition()
                ));
            }
            ProductGroupVendorCondition condition = (ProductGroupVendorCondition)Conditions[EnumDefine.ProductGroupConfigTypeEnum.Vendor].Config;
            condition.Remove(id);
            Conditions[EnumDefine.ProductGroupConfigTypeEnum.Vendor].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void AddWarehouse(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            var key = EnumDefine.ProductGroupConfigTypeEnum.Warehouse;
            if (!Conditions.ContainsKey(key))
            {
                Conditions.Add(key, new ProductGroupCondition(key, new ProductGroupWarehouseCondition()));
            }
            ProductGroupWarehouseCondition condition = (ProductGroupWarehouseCondition)Conditions[key].Config;
            condition.Add(id);
            Conditions[key].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void RemoveWarehouse(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            var key = EnumDefine.ProductGroupConfigTypeEnum.Warehouse;
            if (!Conditions.ContainsKey(key))
            {
                Conditions.Add(key, new ProductGroupCondition(key, new ProductGroupWarehouseCondition()));
            }
            ProductGroupWarehouseCondition condition = (ProductGroupWarehouseCondition)Conditions[key].Config;
            condition.Remove(id);
            Conditions[key].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void AddAttribute(ProductGroupAttributeAddCommand command)
        {
            InitConditions();
            if (!Conditions.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Attribute))
            {
                Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Attribute, new ProductGroupCondition(
                    EnumDefine.ProductGroupConfigTypeEnum.Attribute,
                    new ProductGroupAttributeCondition()
                ));
            }
            ProductGroupAttributeCondition condition = (ProductGroupAttributeCondition)Conditions[EnumDefine.ProductGroupConfigTypeEnum.Attribute].Config;
            condition.Add(command.AttributeId, command.AttributeValueIds);
            Conditions[EnumDefine.ProductGroupConfigTypeEnum.Attribute].ChangeConfig(condition);
            UpdatedUid = command.UpdatedUid;
            UpdatedDateUtc = command.CreatedDateUtc;
        }

        public void ChangeAttribute(ProductGroupAttributeAddCommand command)
        {
            InitConditions();
            if (!Conditions.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Attribute))
            {
                Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Attribute, new ProductGroupCondition(
                    EnumDefine.ProductGroupConfigTypeEnum.Attribute,
                    new ProductGroupAttributeCondition()
                ));
            }
            ProductGroupAttributeCondition condition = (ProductGroupAttributeCondition)Conditions[EnumDefine.ProductGroupConfigTypeEnum.Attribute].Config;
            condition.Change(command.AttributeId, command.AttributeValueIds);
            Conditions[EnumDefine.ProductGroupConfigTypeEnum.Attribute].ChangeConfig(condition);
            UpdatedUid = command.UpdatedUid;
            UpdatedDateUtc = command.CreatedDateUtc;
        }

        public void RemoveAttribute(ProductGroupAttributeRemoveCommand command)
        {
            InitConditions();
            if (!Conditions.ContainsKey(EnumDefine.ProductGroupConfigTypeEnum.Attribute))
            {
                Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Attribute, new ProductGroupCondition(
                    EnumDefine.ProductGroupConfigTypeEnum.Attribute,
                    new ProductGroupAttributeCondition()
                ));
            }
            ProductGroupAttributeCondition condition = (ProductGroupAttributeCondition)Conditions[EnumDefine.ProductGroupConfigTypeEnum.Attribute].Config;
            condition.Remove(command.AttributeId);
            Conditions[EnumDefine.ProductGroupConfigTypeEnum.Attribute].ChangeConfig(condition);
            UpdatedUid = command.UpdatedUid;
            UpdatedDateUtc = command.CreatedDateUtc;
        }

        public void ChangePrice(decimal[] prices)
        {
            InitConditions();
            Conditions.Remove(EnumDefine.ProductGroupConfigTypeEnum.Price);
            if (prices?.Length <= 0)
            {
                return;
            }
            Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Price, new ProductGroupCondition(EnumDefine.ProductGroupConfigTypeEnum.Price, new ProductPriceCondition(prices)));
        }

        public void ChangeQuantity(int[] quantities)
        {
            InitConditions();
            Conditions.Remove(EnumDefine.ProductGroupConfigTypeEnum.Quantity);
            if (quantities?.Length <= 0)
            {
                return;
            }
            Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Quantity, new ProductGroupCondition(EnumDefine.ProductGroupConfigTypeEnum.Quantity, new ProductQuantityCondition(quantities)));
        }

        public void ChangeProduct(string[] ids)
        {
            InitConditions();
            Conditions.Remove(EnumDefine.ProductGroupConfigTypeEnum.Product);
            if (ids?.Length <= 0)
            {
                return;
            }
            Conditions.Add(EnumDefine.ProductGroupConfigTypeEnum.Product, new ProductGroupCondition(EnumDefine.ProductGroupConfigTypeEnum.Product, new ProductIdsCondition(ids)));
        }

        public void AddProduct(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            var key = EnumDefine.ProductGroupConfigTypeEnum.Product;
            if (!Conditions.ContainsKey(key))
            {
                Conditions.Add(key, new ProductGroupCondition(key, new ProductIdsCondition()));
            }
            ProductIdsCondition condition = (ProductIdsCondition)Conditions[key].Config;
            condition.Add(id);
            Conditions[key].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        public void RemoveProduct(string id, string updatedUid, DateTime updatedDate)
        {
            InitConditions();
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            var key = EnumDefine.ProductGroupConfigTypeEnum.Product;
            if (!Conditions.ContainsKey(key))
            {
                Conditions.Add(key, new ProductGroupCondition(key, new ProductIdsCondition()));
            }
            ProductIdsCondition condition = (ProductIdsCondition)Conditions[key].Config;
            condition.Remove(id);
            Conditions[key].ChangeConfig(condition);
            UpdatedUid = updatedUid;
            UpdatedDateUtc = updatedDate;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public string Description { get; private set; }
        public IDictionary<EnumDefine.ProductGroupConfigTypeEnum, ProductGroupCondition> Conditions { get; private set; }

        #endregion
    }
    public class ProductGroupCondition
    {
        public ProductGroupCondition(EnumDefine.ProductGroupConfigTypeEnum configType, object config)
        {
            ConfigType = configType;
            Config = config;
        }
        public EnumDefine.ProductGroupConfigTypeEnum ConfigType { get; private set; }

        private string _config;

        public object Config
        {
            get
            {
                switch (ConfigType)
                {
                    case EnumDefine.ProductGroupConfigTypeEnum.Category:
                        return Common.Serialize.JsonDeserializeObject<ProductGroupCategoryCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Manufacturer:
                        return Common.Serialize.JsonDeserializeObject<ProductGroupManufacturerCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Vendor:
                        return Common.Serialize.JsonDeserializeObject<ProductGroupVendorCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Warehouse:
                        return Common.Serialize.JsonDeserializeObject<ProductGroupWarehouseCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Attribute:
                        return Common.Serialize.JsonDeserializeObject<ProductGroupAttributeCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Price:
                        return Common.Serialize.JsonDeserializeObject<ProductPriceCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Quantity:
                        return Common.Serialize.JsonDeserializeObject<ProductQuantityCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Product:
                        return Common.Serialize.JsonDeserializeObject<ProductIdsCondition>(_config);
                }
                return null;
            }
            private set => _config = Common.Serialize.JsonSerializeObject(value);
        }

        public void ChangeConfig(object config)
        {
            Config = config;
        }
    }

    public class ProductGroupCategoryCondition
    {
        public ProductGroupCategoryCondition(string[] ids)
        {
            Ids = ids;
        }

        public string[] Ids { get; private set; }
    }

    public class ProductGroupManufacturerCondition
    {
        public ProductGroupManufacturerCondition(IList<string> ids)
        {
            Ids = ids;
        }

        public IList<string> Ids { get; private set; }

        public void Add(string id)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (Ids.Any(p => p == id))
            {
                throw new Exception("Manufacturer is exist.");
            }
            Ids.Add(id);
        }
        public void Remove(string id)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (!Ids.Remove(id))
            {
                throw new Exception("Manufacturer not exist");
            }
        }
    }

    public class ProductGroupVendorCondition
    {
        public ProductGroupVendorCondition()
        {
            Ids = new List<string>();
        }
        public ProductGroupVendorCondition(IList<string> ids)
        {
            Ids = ids;
        }

        public IList<string> Ids { get; private set; }

        public void Add(string vendorId)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (Ids.Any(p => p == vendorId))
            {
                throw new Exception("Vender is exist.");
            }
            Ids.Add(vendorId);
        }
        public void Remove(string vendorId)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (!Ids.Remove(vendorId))
            {
                throw new Exception("Vender not exist");
            }
        }
    }

    public class ProductGroupWarehouseCondition
    {
        public ProductGroupWarehouseCondition()
        {
            Ids = new List<string>();
        }
        public ProductGroupWarehouseCondition(IList<string> ids)
        {
            Ids = ids;
        }
        public IList<string> Ids { get; private set; }

        public void Add(string warehouseId)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (Ids.Any(p => p == warehouseId))
            {
                throw new Exception("Warehouse is exist.");
            }
            Ids.Add(warehouseId);
        }
        public void Remove(string warehouseId)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (!Ids.Remove(warehouseId))
            {
                throw new Exception("Warehouse not exist");
            }
        }
    }

    public class ProductGroupAttributeCondition
    {
        public ProductGroupAttributeCondition()
        {

        }
        public ProductGroupAttributeCondition(IList<ProductGroupAttributeDetailCondition> attributes)
        {
            Attributes = attributes;
        }

        public IList<ProductGroupAttributeDetailCondition> Attributes { get; set; }
        public void Add(string attributeId, string[] attributeValueIds)
        {
            if (Attributes == null)
            {
                Attributes = new List<ProductGroupAttributeDetailCondition>();
            }
            var attributeConfig = Attributes.FirstOrDefault(p => p.AttributeId == attributeId);
            if (attributeConfig == null)
            {
                attributeConfig = new ProductGroupAttributeDetailCondition(attributeId, attributeValueIds);
                Attributes.Add(attributeConfig);
            }
            else
            {
                throw new Exception("Attribute is exist.");
            }
        }
        public void Change(string attributeId, string[] attributeValueIds)
        {
            if (Attributes == null)
            {
                Attributes = new List<ProductGroupAttributeDetailCondition>();
            }
            var attributeConfig = Attributes.FirstOrDefault(p => p.AttributeId == attributeId);
            if (attributeConfig == null)
            {
                throw new Exception("Attribute is not exist.");
            }
            else
            {
                attributeConfig.ChangeAttributeValue(attributeValueIds);
            }
        }
        public void Remove(string attributeId)
        {
            if (Attributes == null)
            {
                Attributes = new List<ProductGroupAttributeDetailCondition>();
            }
            var attributeConfig = Attributes.FirstOrDefault(p => p.AttributeId == attributeId);
            if (attributeConfig == null)
            {
                throw new Exception("Attribute not exist");
            }
            else
            {
                Attributes.Remove(attributeConfig);
            }
        }
    }

    public class ProductGroupAttributeDetailCondition
    {
        public ProductGroupAttributeDetailCondition(string attributeId, string[] attributeValueIds)
        {
            AttributeId = attributeId;
            AttributeValueIds = attributeValueIds;
        }

        public string AttributeId { get; private set; }
        public string[] AttributeValueIds { get; private set; }

        public void ChangeAttributeValue(string[] attributeValueIds)
        {
            AttributeValueIds = attributeValueIds;
        }
    }

    public class ProductPriceCondition
    {
        public ProductPriceCondition(decimal[] prices)
        {
            Prices = prices;
        }

        public decimal[] Prices { get; private set; }

        public void ChangePrice(decimal[] prices)
        {
            Prices = prices;
        }
    }

    public class ProductQuantityCondition
    {
        public ProductQuantityCondition(int[] quantities)
        {
            Quantities = quantities;
        }

        public int[] Quantities { get; private set; }
    }

    public class ProductIdsCondition
    {
        public ProductIdsCondition()
        {
            Ids = new List<string>();
        }
        public ProductIdsCondition(IList<string> ids)
        {
            Ids = ids;
        }

        public IList<string> Ids { get; private set; }

        public void Add(string productId)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (Ids.Any(p => p == productId))
            {
                throw new Exception("Product is exist.");
            }
            Ids.Add(productId);
        }
        public void Remove(string productId)
        {
            if (Ids == null)
            {
                Ids = new List<string>();
            }
            if (!Ids.Remove(productId))
            {
                throw new Exception("Product not exist");
            }
        }
    }
}
