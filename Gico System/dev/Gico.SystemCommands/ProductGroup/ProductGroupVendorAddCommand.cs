using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupVendorAddCommand : Command
    {
        public ProductGroupVendorAddCommand()
        {
        }
        public string VendorId { get; set; }
        public string ProductGroupId { get; set; }
        public string UpdatedUid { get; set; }
    }
}