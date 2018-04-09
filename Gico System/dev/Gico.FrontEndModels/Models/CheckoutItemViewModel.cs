using Gico.Config;

namespace Gico.FrontEndModels.Models
{
    public class CheckoutItemViewModel
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public EnumDefine.ProductTypeEnum ProductType { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}