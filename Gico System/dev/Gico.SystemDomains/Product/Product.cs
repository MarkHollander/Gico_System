using Gico.Config;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;
using Gico.ReadSystemModels;
using Gico.ReadSystemModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gico.SystemDomains.Product
{
    public class Product : BaseDomain, IVersioned
    {
        public Product(int version)
        {
            Version = version;
        }

        public Product(RProduct product)
        {
            Id = product.Id;
            Type = product.Type;
            ParentId = product.ParentId;
            Name = product.Name;
            IsBaseProduct = product.IsBaseProduct;
            Images = product.Images;
            ShortDescription = product.ShortDescription;
            FullDescription = product.FullDescription;
            Status = product.Status;
            AllowCustomerReviews = product.AllowCustomerReviews;
            ManufacturerPartNumber = product.ManufacturerPartNumber;
            Gtin = product.Gtin;
            HasUserAgreement = product.HasUserAgreement;
            UserAgreementText = product.UserAgreementText;
            TaxCategoryId = product.TaxCategoryId;
            DisplayStockAvailability = product.DisplayStockAvailability;
            DisplayStockQuantity = product.DisplayStockQuantity;
            AllowBackInStockSubscriptions = product.AllowBackInStockSubscriptions;
            OrderMinimumQuantity = product.OrderMinimumQuantity;
            OrderMaximumQuantity = product.OrderMaximumQuantity;
            NotReturnable = product.NotReturnable;
            DisableBuyButton = product.DisableBuyButton;
            DisableWishlistButton = product.DisableWishlistButton;
            AvailableForPreOrder = product.AvailableForPreOrder;
            PreOrderAvailabilityStartDateTimeUtc = product.PreOrderAvailabilityStartDateTimeUtc;
            Weight = product.Weight;
            Length = product.Length;
            Width = product.Width;
            Height = product.Height;
            SerialRequired = product.SerialRequired;
            InstallRequired = product.InstallRequired;
            AvailableStartDateTimeUtc = product.AvailableStartDateTimeUtc;
            AvailableEndDateTimeUtc = product.AvailableEndDateTimeUtc;
            ProductUnit = product.ProductUnit;
            QtyPerUnit = product.QtyPerUnit;
            DisplayOption = product.DisplayOption;
            Version = product.Version;
            SeoInfo = product.SeoInfo;
            UpdatedDateUtc = product.UpdatedDateUtc;
            CreatedDateUtc = product.CreatedDateUtc;
            CreatedUid = product.CreatedUid;
            UpdatedDateUtc = product.UpdatedDateUtc;
        }


        #region Instance Properties
        public EnumDefine.ProductType Type { get; private set; }
        public string ParentId { get; private set; }
        public string Name { get; private set; }
        public bool IsBaseProduct { get; private set; }
        public string Images { get; private set; }
        public string ShortDescription { get; private set; }
        public string FullDescription { get; private set; }
        public new EnumDefine.ProductStatus Status { get; private set; }
        public bool AllowCustomerReviews { get; private set; }
        public string ManufacturerPartNumber { get; private set; }
        public string Gtin { get; private set; }
        public bool HasUserAgreement { get; private set; }
        public string UserAgreementText { get; private set; }
        public string TaxCategoryId { get; private set; }
        public bool DisplayStockAvailability { get; private set; }
        public bool DisplayStockQuantity { get; private set; }
        public bool AllowBackInStockSubscriptions { get; private set; }
        public int OrderMinimumQuantity { get; private set; }
        public int OrderMaximumQuantity { get; private set; }
        public bool NotReturnable { get; private set; }
        public bool DisableBuyButton { get; private set; }
        public bool DisableWishlistButton { get; private set; }
        public bool AvailableForPreOrder { get; private set; }
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; private set; }
        public Decimal Weight { get; private set; }
        public Decimal Length { get; private set; }
        public Decimal Width { get; private set; }
        public Decimal Height { get; private set; }
        public bool SerialRequired { get; private set; }
        public bool InstallRequired { get; private set; }
        public DateTime? AvailableStartDateTimeUtc { get; private set; }
        public DateTime? AvailableEndDateTimeUtc { get; private set; }
        public string ProductUnit { get; private set; }
        public Decimal QtyPerUnit { get; private set; }
        public string DisplayOption { get; private set; }
        public int Version { get; private set; }
        public string SeoInfo { get; private set; }

        public List<Product_Category_Mapping> ProductCategoryMappings { get; private set; }
        public List<Product_Manufacturer_Mapping> ProductManufacturerMappings { get; private set; }
        public List<Product_ProductAttribute_Mapping> ProductProductAttributeMappings { get; private set; }
        public List<Vendor_Product_Mapping> VendorProductMappings { get; private set; }
        public List<Warehouse_Product_Mapping> WarehouseProductMappings { get; private set; }
        #endregion Instance Properties
    }
}
