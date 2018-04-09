using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupManufacturAddCommand : Command
    {
        public ProductGroupManufacturAddCommand()
        {
        }
        public string ManufacturerId { get; set; }
        public string ProductGroupId { get; set; }
        public string UpdatedUid { get; set; }
    }
}