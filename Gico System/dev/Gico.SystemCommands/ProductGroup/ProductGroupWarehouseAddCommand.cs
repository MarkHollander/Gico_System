using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupWarehouseAddCommand : Command
    {
        public ProductGroupWarehouseAddCommand()
        {
        }
        public string WarehouseId { get; set; }
        public string ProductGroupId { get; set; }
        public string UpdatedUid { get; set; }
    }
}