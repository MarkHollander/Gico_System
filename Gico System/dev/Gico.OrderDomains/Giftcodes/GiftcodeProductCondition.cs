namespace Gico.OrderDomains.Giftcodes
{
    public class GiftcodeProductCondition
    {
        public int ProductItemId { get; set; }
        public int WarehouseId { get; set; }
        public int MerchantId { get; set; }
        public int ProductType { get; set; }
        public string ProductName { get; set; }
        public string MerchantName { get; set; }
        public string WarehouseName { get; set; }
        public string ProductUrl { get; set; }
    }
}