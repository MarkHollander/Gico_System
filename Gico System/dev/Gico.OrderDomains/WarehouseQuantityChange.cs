namespace Gico.OrderDomains
{
    public class WarehouseQuantityChange
    {
        public WarehouseQuantityChange(string warehouseId, string productId, decimal price, int quantity)
        {
            WarehouseId = warehouseId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public void QuantityUp(int up)
        {
            Quantity += up;
        }
        public string WarehouseId { get; private set; }
        public string ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}