using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class WarehouseProductMappingAddCommand : Command
    {
        public WarehouseProductMappingAddCommand()
        {
        }
        public string VendorProductId { get; set; }
        public string WarehouseId { get; set; }
        public int Quantity { get; set; }
        public decimal SellPrice { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public int SafetyStock { get; set; }
        public DateTime? StartDateTimeUtc { get; set; }
        public DateTime? EndDateTimeUtc { get; set; }
        public int QtyReserved { get; set; }
        public string ProductId { get; set; }
        public string CreatedUid { get; set; }
    }
}
