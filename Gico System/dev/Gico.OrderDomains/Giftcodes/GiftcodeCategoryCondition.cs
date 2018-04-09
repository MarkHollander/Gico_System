namespace Gico.OrderDomains.Giftcodes
{
    public class GiftcodeCategoryCondition
    {
        public int CategoryId { get; set; }
        public int ProductType { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
    }
}