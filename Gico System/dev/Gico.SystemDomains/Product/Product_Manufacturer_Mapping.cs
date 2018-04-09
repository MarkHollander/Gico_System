using Gico.Domains;
using Gico.ReadSystemModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemDomains.Product
{
    public class Product_Manufacturer_Mapping : BaseDomain
    {
        public Product_Manufacturer_Mapping()
        {
        }

        public Product_Manufacturer_Mapping(RProduct_Manufacturer_Mapping productManufacturerMapping)
        {
            ProductId = productManufacturerMapping.ProductId;
            ManufacturerId = productManufacturerMapping.ManufacturerId;            
        }
        #region Instance Properties
        public string ProductId { get; private set; }
        public int ManufacturerId { get; private set; }
        #endregion Instance Properties
    }
}
