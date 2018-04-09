using System.Linq;
using Gico.Config;

namespace Gico.FrontEndModels.Models
{
    public class CheckoutViewModel : PageModel
    {
        public CheckoutViewModel()
        {
            Addresses = new AddressViewModel[0];
        }

        public CheckoutViewModel(PageModel model) : base(model)
        {
            Addresses = new AddressViewModel[0];
        }

        public bool IsLogin { get; set; }
        public AddressViewModel[] Addresses { get; set; }
        public DeliveryTime[] DeliveryTimes { get; set; }

        public AddressViewModel BillingAddress { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public string TaxCode { get; set; }
        public string TaxCompanyName { get; set; }
        public string TaxAddress { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public int Version { get; set; }
        public string ClientId { get; set; }
        public PaidAdvanceViewModel[] PaidAdvances { get; set; }
        public CheckoutItemViewModel[] CheckoutItems { get; set; }

        public decimal TotalPrice => CheckoutItems.Where(p => p.ProductType == EnumDefine.ProductTypeEnum.Product).Sum(p => p.TotalPrice);
        public decimal DeliveryCharge => CheckoutItems.Where(p => p.ProductType == EnumDefine.ProductTypeEnum.DeliveryCharge).Sum(p => p.TotalPrice);
        public decimal TotalPaidAmount => TotalPrice + DeliveryCharge;
    }
}