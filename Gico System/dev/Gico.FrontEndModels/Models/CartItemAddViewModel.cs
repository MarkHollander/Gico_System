namespace Gico.FrontEndModels.Models
{
    public class CartItemChangeViewModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Action { get; set; }
        public int Version { get; set; }
        public int ShardId { get; set; }
    }

    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }

    }
}