using System.Collections.Generic;
using Gico.Domains;

namespace Gico.OrderDomains
{
    public class OrderItem : BaseDomain
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string ProductId { get; set; }
        public List<PaymentDetail> PaymentDetails { get; set; }
        
    }
}