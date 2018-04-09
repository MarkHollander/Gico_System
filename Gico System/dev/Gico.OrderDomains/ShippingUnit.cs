using Gico.Domains;

namespace Gico.OrderDomains
{
    public class ShippingUnit:BaseDomain
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        
    }
}