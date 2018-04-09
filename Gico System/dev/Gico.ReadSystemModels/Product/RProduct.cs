using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.ReadSystemModels;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.ReadSystemModels.Product
{
    [ProtoContract]
    public class RProduct : BaseReadModel
    {
        #region Instance Properties
        [ProtoMember(1)]
        public EnumDefine.ProductType Type { get; set; }
        [ProtoMember(2)]
        public string ParentId { get; set; }
        [ProtoMember(3)]
        public string Name { get; set; }
        [ProtoMember(4)]
        public bool IsBaseProduct { get; set; }
        [ProtoMember(5)]
        public string Images { get; set; }
        [ProtoMember(6)]
        public string ShortDescription { get; set; }
        [ProtoMember(7)]
        public string FullDescription { get; set; }
        [ProtoMember(8)]
        public new EnumDefine.ProductStatus Status { get; set; }
        [ProtoMember(9)]
        public bool AllowCustomerReviews { get; set; }
        [ProtoMember(10)]
        public string ManufacturerPartNumber { get; set; }
        [ProtoMember(11)]
        public string Gtin { get; set; }
        [ProtoMember(12)]
        public bool HasUserAgreement { get; set; }
        [ProtoMember(13)]
        public string UserAgreementText { get; set; }
        [ProtoMember(14)]
        public string TaxCategoryId { get; set; }
        [ProtoMember(15)]
        public bool DisplayStockAvailability { get; set; }
        [ProtoMember(16)]
        public bool DisplayStockQuantity { get; set; }
        [ProtoMember(17)]
        public bool AllowBackInStockSubscriptions { get; set; }
        [ProtoMember(18)]
        public int OrderMinimumQuantity { get; set; }
        [ProtoMember(19)]
        public int OrderMaximumQuantity { get; set; }
        [ProtoMember(20)]
        public bool NotReturnable { get; set; }
        [ProtoMember(21)]
        public bool DisableBuyButton { get; set; }
        [ProtoMember(22)]
        public bool DisableWishlistButton { get; set; }
        [ProtoMember(23)]
        public bool AvailableForPreOrder { get; set; }
        [ProtoMember(24)]
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }
        [ProtoMember(25)]
        public Decimal Weight { get; set; }
        [ProtoMember(26)]
        public Decimal Length { get; set; }
        [ProtoMember(27)]
        public Decimal Width { get; set; }
        [ProtoMember(28)]
        public Decimal Height { get; set; }
        [ProtoMember(29)]
        public bool SerialRequired { get; set; }
        [ProtoMember(30)]
        public bool InstallRequired { get; set; }
        [ProtoMember(31)]
        public DateTime? AvailableStartDateTimeUtc { get; set; }
        [ProtoMember(32)]
        public DateTime? AvailableEndDateTimeUtc { get; set; }
        [ProtoMember(33)]
        public string ProductUnit { get; set; }
        [ProtoMember(34)]
        public Decimal QtyPerUnit { get; set; }
        [ProtoMember(35)]
        public string DisplayOption { get; set; }
        [ProtoMember(36)]
        public string SeoInfo { get; set; }
        #endregion Instance Properties
    }
}
