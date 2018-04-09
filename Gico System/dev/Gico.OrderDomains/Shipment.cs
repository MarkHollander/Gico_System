using System.Collections.Generic;

namespace Gico.OrderDomains
{
    public class Shipment
    {
        public List<ShipmentItem> ShipmentItems { get; set; }
        public ShippingUnit ShippingUnit { get; set; }
    }
}