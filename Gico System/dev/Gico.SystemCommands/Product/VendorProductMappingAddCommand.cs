using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class VendorProductMappingAddCommand : Command
    {
        public VendorProductMappingAddCommand()
        {
        }

        public string ProductId { get; set; }
        public string VendorId { get; set; }
        public string VendorSku { get; set; }
        public decimal OriginalPrice { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public decimal VAT { get; set; }
        public decimal VatEx { get; set; }
        public string Barcode { get; set; }
        public int IsVisible { get; set; }
        public string CreatedUid { get; set; }
    }
}
