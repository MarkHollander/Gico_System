using System;
using System.ComponentModel.DataAnnotations;

namespace Gico.Config
{
    public class EnumDefine
    {

        public enum CategoryStatus
        {
            [Display(Name = "Tạo mới")]
            New = 1,
            [Display(Name = "Đã duyệt")]
            Active = 2,
            [Display(Name = "Đã xóa")]
            Remove = 3

        }

        public enum VariationThemeStatus
        {
            [Display(Name = "Đã duyệt")]
            Active = 1,
            [Display(Name = "Đã xóa")]
            Remove = 2

        }

        public enum AttrCategoryType
        {
            [Display(Name = "Chọn một giá trị")]
            SelectOneValue = 1,
            [Display(Name = "Text")]
            Text = 2
        }

        public enum GenderEnum
        {
            Male = 1,
            Female = 2,
            Other = 3
        }
        public enum CustomerStatusEnum
        {
            New = 1,
            Active = 2,
            Lock = 3
        }
        [Flags]
        public enum CustomerTypeEnum
        {
            IsEmployee = 1,
            IsCustomer = 2,
            IsCustomerVip1 = 4,
            IsCustomerVip2 = 8,
        }
        public enum CutomerExternalLoginProviderEnum
        {
            Facebook = 1,
            Google = 2
        }
        public enum CustomerExternalLoginCallbackStatusEnum
        {
            LoginFail = 1,
            AccountNotExist = 2,
            LoginSuccess = 3,
            AccountIsExist = 4
        }
        public enum VendorStatusEnum
        {
            New = 1,
            Active = 2,
            Lock = 3
        }
        public enum WarehouseStatusEnum
        {
            New = 1,
            Active = 2,
            Lock = 3
        }

        public enum WarehouseTypeEnum
        {
            [Display(Name ="Private")]
            Private = 1,
            [Display(Name = "Public")]
            Public = 2,
            [Display(Name = "Automated")]
            Automated = 3,
            [Display(Name = "Climate controlled")]
            Climate_Controlled = 4,
            [Display(Name = "Distribution")]
            Distribution = 5
        }

        [Flags]
        public enum VendorTypeEnum
        {
            IsA = 16,
            IsB = 32,
        }
        public enum TwoFactorEnum
        {
            Disable = -1,
            Enable = 1
        }

        public enum StatusEnum
        {
            Delete = -1,
            New = 1,
            Active = 2,
            Lock = 3,

        }
        [Flags]
        public enum TypeEnum
        {
            None = 0,
            IsEmployee = 1,
            IsCustomer = 2,
            IsCustomerVip1 = 4,
            IsCustomerVip2 = 8,
            IsA = 16,
            IsB = 32
        }

        public enum CartStatusEnum
        {
            AllStatus = 0,
            New = 1,
            Remove = -1,
            NotInStock = 2,
            NotEnoughQuantity = 3,
            EnoughInventory = 4,
            NotShippingSupport = 5,
            IsVenderHoliday = 6,
        }
        public enum CartActionEnum
        {
            AddNewItem = 1,
            RemoveItem = 2,
            ChangeQuantity = 3,

        }
        [Flags]
        public enum ShardStatusEnum
        {
            New = 1,
            Active = 2,
            IsWrite = 4,

        }
        public enum ShardTypeEnum
        {
            RoundRobin = 1,
            Hash = 2,
            Range = 3,
            Month = 4,
            Year = 5
        }
        public enum ShardGroupEnum
        {
            None = 0,
            Order = 1,
            EventSourcing = 2,
            File = 3,
            User = 4,
            Sharding = 5
        }
        public enum PiadAdvanceTypeEnum
        {
            // ReSharper disable once InconsistentNaming
            COD = 1,
            // ReSharper disable once InconsistentNaming
            SMLATM = 2,
            // ReSharper disable once InconsistentNaming
            SMLVISA = 3,
            // ReSharper disable once InconsistentNaming
            ONEPAYATM = 4,
            // ReSharper disable once InconsistentNaming
            ONEPAYVISA = 5,
            // ReSharper disable once InconsistentNaming
            GIFTCODE = 6,
            // ReSharper disable once InconsistentNaming
            GICOPOINT = 7,
            // ReSharper disable once InconsistentNaming
            GICOCARD = 8
        }
        public enum ProductTypeEnum
        {
            Product = 1,
            DeliveryCharge = 2
        }

        public enum EsMethodName
        {
            // ReSharper disable once InconsistentNaming
            _search = 1,
            // ReSharper disable once InconsistentNaming
            _create = 2,
            // ReSharper disable once InconsistentNaming
            _bulk = 3,
            // ReSharper disable once InconsistentNaming
            _update = 4
        }
        public enum EsIndexName
        {
            AddressesBase = 1,
        }
        public enum EsIndexType
        {
            AddressBase = 1,
        }
        public enum DepartmentStatusEnum
        {
            Active = 1,
            Deleted = -1
        }
        public enum RoleStatusEnum
        {
            Active = 1,
            Deleted = -1
        }




        #region Giftcode
        public enum GiftCodeCampaignStatus
        {
            [Display(Name = "Tạo mới")]
            New = 1,
            [Display(Name = "Đã duyệt")]
            Active = 2,
            [Display(Name = "Tạm dừng")]
            Stop = 3,
            [Display(Name = "Đã xóa")]
            Remove = 4,
            [Display(Name = "Từ chối")]
            Cancel = 5,
            [Display(Name = "Chờ duyệt lại")]
            ReActive = 6
        }
        public enum GiftCodeGroupStatus
        {
            [Display(Name = "Tạo mới")]
            New = 1,
            [Display(Name = "Đã duyệt")]
            Active = 2,
            [Display(Name = "Tạm dừng")]
            Stop = 3,
            [Display(Name = "Đã xóa")]
            Remove = 4,
            [Display(Name = "Từ chối")]
            Cancel = 5,
            [Display(Name = "Chờ duyệt lại")]
            ReActive = 6
        }
        public enum GiftCodeActionTypeEnum
        {
            Active = 1,
            Stop = 2,
            GiftCodeReCreate = 3,
            GiftCodeRemove = 4,
            GiftCodeAdd = 5,
            GiftCodeRemoveByGroup = 6,
        }
        public enum GiftcodeGroupTypeEnum
        {
            [Display(Name = "(n) mã, mỗi mã 1 lần gắn với 1 định danh")]
            OnceUsed = 1,
            [Display(Name = "(n) mã, mỗi mã (m) lần với 1 định danh")]
            OnceUsedByAccount = 2,
            [Display(Name = "1 mã, (m) lần với (z) định danh")]
            RepeatedUse = 3,
        }
        public enum GiftCodeTypeEum
        {
            [Display(Name = "Giảm theo %")]
            Percent = 1,
            [Display(Name = "Giảm theo số tiền")]
            Amount = 2
        }
        public enum GiftCodeConditionTypeEnum
        {
            Product = 1,
            Category = 2,
            Price = 3,
            PaymentType = 4,
            DeviceType = 5,
            Province = 6,
            UseByOtherPromotion = 7,
            Vender = 8,
            MaxUsingCountByUser = 9,
            Email = 10,
            Mobile = 11,
            NewCustomer = 14
        }
        public enum GiftCodeMerchantTypeCondition
        {
            [Display(Name = "Tất cả")]
            ALL = 0,
            [Display(Name = "Bao gồm")]
            Accept = 1,
            [Display(Name = "Không bao gồm")]
            NotAccept = 2

        }
        public enum GiftCodeUsedStatus
        {
            [Display(Name = "Đã sử dụng")]
            Used = 1,
            [Display(Name = "Đang tạm giữ")]
            Holding = 2,
            [Display(Name = "Hủy")]
            Cancel = 3
        }
        public enum ConditionDetailTypeEnum
        {
            Email = 1,
            Mobile = 2,
            RequireOTP = 10
        }
        #endregion

        #region Page builder
        public enum CommonStatusEnum
        {
            [Display(Name = "Duyệt")]
            Active = 1,
            [Display(Name = "Xóa")]
            Deleted = -1,
            [Display(Name = "Tạo mới")]
            New = 3,
            [Display(Name = "Ẩn")]
            InActive = 2
        }
        public enum TemplatePageTypeEnum
        {
            [Display(Name = "Home")]
            Home = 1,
            [Display(Name = "Category")]
            Category = 2,
            [Display(Name = "Detail")]
            Menu = 3,
            [Display(Name = "Brand")]
            Brand = 4,
            [Display(Name = "LandingPage")]
            LandingPage = 5
        }
        public enum TemplateConfigComponentTypeEnum
        {
            [Display(Name = "Banner")]
            Banner = 1,
            [Display(Name = "ProductGroup")]
            ProductGroup = 2,
            [Display(Name = "Menu")]
            Menu = 3
        }
        #endregion

        #region Product

        public enum ProductStatus
        {
            New = 1
        }

        public enum ProductType
        {
            [Display(Name = "Bình thường")]
            Normal = 1,
            [Display(Name = "Tươi sống")]
            FreshFood = 2,
            [Display(Name = "Cồng kềnh")]
            Bulky = 3
        }

        #endregion

        #region EmailOrSms

        public enum EmailOrSmsTypeEnum
        {
            [Display(Name = "View.cshtml")]
            ExternalLoginConfirmWhenAccountIsExist = 1
        }
        [Flags]
        public enum EmailOrSmsMessageTypeEnum
        {
            Email = 1,
            Sms = 2
        }
        [Flags]
        public enum EmailOrSmsStatusEnum
        {
            New = 1,
            SendEmailSuccess = 2,
            SendSmsSuccess = 4,
            SendEmailRetry = 8,
            SendSmsRetry = 16,
            SendEmailCancel = 32,
            SendSmsCancel = 64,
        }
        #endregion

        public enum VerifyTypeEnum
        {
            EmailOtp = 1,
            SmsOtp = 2,
            EmailUrl = 3
        }
        [Flags]
        public enum VerifyStatusEnum
        {
            New = 1,
            Send = 2,
            Used = 4,
            Cancel = 8
        }
        #region Menu
        public enum MenuTypeEnum
        {
            Link = 1,
            Category = 2,
            Brand = 3
        }
        [Flags]
        public enum MenuPositionEnum
        {
            Other = 1,
            Header = 2,
            Footer = 4,

        }
        #endregion
        public enum ProductGroupConfigTypeEnum
        {
            Category = 1,
            Manufacturer = 2,
            Vendor = 3,
            Warehouse = 4,
            Attribute = 5,
            Price = 6,
            Quantity = 7,
            Product = 8
        }

      
      
    }

    public class Ref<T>
    {
        public T Value { get; set; }
    }
    public class RefSqlPaging
    {
        public RefSqlPaging(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            if (PageSize <= 0)
            {
                PageSize = 30;
            }
            if (PageSize > 100)
            {
                PageSize = 100;
            }
            if (PageIndex <= 0)
            {
                PageIndex = 0;
            }

        }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        public int OffSet => PageIndex * PageSize;

        public int TotalRow { get; set; }
    }

    public class DateTimeRange
    {
        public DateTimeRange(DateTime? fromDate, DateTime? toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
        public DateTime? FromDate { get; private set; }
        public DateTime? ToDate { get; private set; }
    }
}