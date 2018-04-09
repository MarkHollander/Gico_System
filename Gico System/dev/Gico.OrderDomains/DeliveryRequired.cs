using System;
using Gico.Domains;

namespace Gico.OrderDomains
{
    public class DeliveryRequired : BaseDomain
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        
    }
}