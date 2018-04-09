using Gico.Domains;
using Gico.ReadSystemModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemDomains.Product
{
    public class Product_ProductAttribute_Mapping : BaseDomain
    {
        public Product_ProductAttribute_Mapping()
        {
        }

        public Product_ProductAttribute_Mapping(RProduct_ProductAttribute_Mapping productProductAttributeMapping)
        {
            ProductId = productProductAttributeMapping.ProductId;
            AttributeId = productProductAttributeMapping.AttributeId;
            AttributeValueId = productProductAttributeMapping.AttributeValueId;
            DisplayUnitId = productProductAttributeMapping.DisplayUnitId;
            StringValue = productProductAttributeMapping.StringValue;
            IsSpecificAttribute = productProductAttributeMapping.IsSpecificAttribute;
            IsRequired = productProductAttributeMapping.IsRequired;
            AttributeType = productProductAttributeMapping.AttributeType;
            DisplayOrder = productProductAttributeMapping.DisplayOrder;
        }
        #region Instance Properties
        public string ProductId { get; private set; }
        public int AttributeId { get; private set; }
        public int AttributeValueId { get; private set; }
        public int DisplayUnitId { get; private set; }
        public string StringValue { get; private set; }
        public bool IsSpecificAttribute { get; private set; }
        public bool IsRequired { get; private set; }
        public int AttributeType { get; private set; }
        public int DisplayOrder { get; private set; }
        #endregion Instance Properties
    }
}
