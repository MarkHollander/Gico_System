using Gico.Config;
using Gico.Domains;
using Gico.ReadSystemModels.Product;
using System;

namespace Gico.SystemDomains.Product
{
    public class Warehouse_Product_Mapping : BaseDomain
    {
        public Warehouse_Product_Mapping()
        {
        }

        public Warehouse_Product_Mapping(RWarehouse_Product_Mapping warehouseProductMapping)
        {
            VendorProductId = warehouseProductMapping.VendorProductId;
            WarehouseId = warehouseProductMapping.WarehouseId;
            Quantity = warehouseProductMapping.Quantity;
            SellPrice = warehouseProductMapping.SellPrice;
            Status = warehouseProductMapping.Status;
            SafetyStock = warehouseProductMapping.SafetyStock;
            StartDateTimeUtc = warehouseProductMapping.StartDateTimeUtc;
            EndDateTimeUtc = warehouseProductMapping.EndDateTimeUtc;
            QtyReserved = warehouseProductMapping.QtyReserved;
            ProductId = warehouseProductMapping.ProductId;
        }

        public string VendorProductId { get; private set; }
        public string WarehouseId { get; private set; }
        public int Quantity { get; private set; }
        public decimal SellPrice { get; private set; }
        public new EnumDefine.CommonStatusEnum Status { get; private set; }
        public int SafetyStock { get; private set; }
        public DateTime? StartDateTimeUtc { get; private set; }
        public DateTime? EndDateTimeUtc { get; private set; }
        public int QtyReserved { get; private set; }
        public string ProductId { get; private set; }

    }
}