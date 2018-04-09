using Gico.Config;
using Gico.CQRS.Model.Implements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemCommands.Product
{
    public class ProductAddCommand : Command
    {
        public ProductAddCommand()
        {
        }

        public ProductAddCommand(int version) : base(version)
        {
        }
        public EnumDefine.ProductType Type { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public bool IsBaseProduct { get; set; }
        public string Images { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public EnumDefine.CommonStatusEnum Status { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Gtin { get; set; }
        public bool HasUserAgreement { get; set; }
        public string UserAgreementText { get; set; }
        public string TaxCategoryId { get; set; }
        public bool DisplayStockAvailability { get; set; }
        public bool DisplayStockQuantity { get; set; }
        public bool AllowBackInStockSubscriptions { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public bool NotReturnable { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool DisableWishlistButton { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }
        public Decimal Weight { get; set; }
        public Decimal Length { get; set; }
        public Decimal Width { get; set; }
        public Decimal Height { get; set; }
        public bool SerialRequired { get; set; }
        public bool InstallRequired { get; set; }
        public DateTime? AvailableStartDateTimeUtc { get; set; }
        public DateTime? AvailableEndDateTimeUtc { get; set; }
        public string ProductUnit { get; set; }
        public Decimal QtyPerUnit { get; set; }
        public string DisplayOption { get; set; }        
        public string SeoInfo { get; set; }
        public string CreatedUid { get; set; }

    }
}
