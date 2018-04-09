using Gico.Config;
using Gico.Domains;
using Gico.ReadSystemModels.Product;
using System;
using System.Collections.Generic;

namespace Gico.SystemDomains.Product
{
    public class Vendor_Product_Mapping : BaseDomain
    {
        public Vendor_Product_Mapping()
        {
        }

        public Vendor_Product_Mapping(RVendor_Product_Mapping vendorProductMapping)
        {
            ProductId = vendorProductMapping.ProductId;
            VendorId = vendorProductMapping.VendorId;
            VendorSku = vendorProductMapping.VendorSku;
            OriginalPrice = vendorProductMapping.OriginalPrice;
            Status = vendorProductMapping.Status;
            VAT = vendorProductMapping.VAT;
            VatEx = vendorProductMapping.VatEx;
            Barcode = vendorProductMapping.Barcode;
            IsVisible = vendorProductMapping.IsVisible;
        }

        public string ProductId { get; private set; }
        public string VendorId { get; private set; }
        public string VendorSku { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public decimal VAT { get; private set; }
        public decimal VatEx { get; private set; }        
        public string Barcode { get; private set; }
        public int IsVisible { get; private set; }
        public List<Warehouse_Product_Mapping> WarehouseProductMappings { get; private set; }
    }
}