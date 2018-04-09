using Gico.Config;
using ProtoBuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.ProductGroup
{
    //[ProtoContract]
    public class RProductGroup : BaseReadModel
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public new EnumDefine.CommonStatusEnum Status { get; set; }
        [ProtoMember(3)]
        public string Description { get; set; }
        [ProtoMember(4)]
        public string Conditions { get; set; }

        public IDictionary<EnumDefine.ProductGroupConfigTypeEnum, RProductGroupCondition> ConditionsObject => Common.Serialize
            .JsonDeserializeObject<IDictionary<EnumDefine.ProductGroupConfigTypeEnum, RProductGroupCondition>>(
                Conditions) ?? new ConcurrentDictionary<EnumDefine.ProductGroupConfigTypeEnum, RProductGroupCondition>();


    }
    public class RProductGroupCondition
    {
        public RProductGroupCondition(EnumDefine.ProductGroupConfigTypeEnum configType, object config)
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
                        return Common.Serialize.JsonDeserializeObject<RProductGroupCategoryCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Manufacturer:
                        return Common.Serialize.JsonDeserializeObject<RProductGroupManufacturerCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Vendor:
                        return Common.Serialize.JsonDeserializeObject<RProductGroupVendorCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Warehouse:
                        return Common.Serialize.JsonDeserializeObject<RProductGroupWarehouseCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Attribute:
                        return Common.Serialize.JsonDeserializeObject<RProductGroupAttributeCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Price:
                        return Common.Serialize.JsonDeserializeObject<RProductPriceCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Quantity:
                        return Common.Serialize.JsonDeserializeObject<RProductQuantityCondition>(_config);
                    case EnumDefine.ProductGroupConfigTypeEnum.Product:
                        return Common.Serialize.JsonDeserializeObject<RProductIdsCondition>(_config);
                }
                return null;
            }
            private set => _config = Common.Serialize.JsonSerializeObject(value);
        }
    }
    public class RProductGroupCategoryCondition
    {
        public RProductGroupCategoryCondition(string[] ids)
        {
            Ids = ids;
        }

        public string[] Ids { get; private set; }
    }
    public class RProductGroupManufacturerCondition
    {
        public RProductGroupManufacturerCondition(string[] ids)
        {
            Ids = ids;
        }

        public string[] Ids { get; private set; }
    }
    public class RProductGroupVendorCondition
    {
        public RProductGroupVendorCondition(string[] ids)
        {
            Ids = ids;
        }

        public string[] Ids { get; private set; }
    }
    public class RProductGroupWarehouseCondition
    {
        public RProductGroupWarehouseCondition(string[] ids)
        {
            Ids = ids;
        }

        public string[] Ids { get; private set; }
    }
    public class RProductGroupAttributeCondition
    {
        public RProductGroupAttributeCondition(RProductGroupAttributeDetailCondition[] attributes)
        {
            Attributes = attributes;
        }

        public RProductGroupAttributeDetailCondition[] Attributes { get; private set; }
    }
    public class RProductGroupAttributeDetailCondition
    {
        public RProductGroupAttributeDetailCondition(string attributeId, string[] attributeValueIds)
        {
            AttributeId = attributeId;
            AttributeValueIds = attributeValueIds;
        }

        public string AttributeId { get; private set; }
        public string[] AttributeValueIds { get; private set; }
    }
    public class RProductPriceCondition
    {
        public RProductPriceCondition(decimal[] prices)
        {
            Prices = prices;
        }

        public decimal[] Prices { get; private set; }
    }
    public class RProductQuantityCondition
    {
        public RProductQuantityCondition(int[] quantities)
        {
            Quantities = quantities;
        }

        public int[] Quantities { get; private set; }
    }
    public class RProductIdsCondition
    {
        public RProductIdsCondition(string[] ids)
        {
            Ids = ids;
        }

        public string[] Ids { get; private set; }
    }
}
