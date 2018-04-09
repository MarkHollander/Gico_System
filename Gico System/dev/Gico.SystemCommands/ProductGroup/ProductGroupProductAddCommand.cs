using Gico.CQRS.Model.Implements;

namespace Gico.SystemCommands.ProductGroup
{
    public class ProductGroupProductAddCommand : Command
    {
        public ProductGroupProductAddCommand()
        {
        }
        public string ProductId { get; set; }
        public string ProductGroupId { get; set; }
        public string UpdatedUid { get; set; }
    }
}