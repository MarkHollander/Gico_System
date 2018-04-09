using System.Collections.Generic;
using Gico.Domains;

namespace Gico.OrderDomains
{
    public class ReturnOrder:BaseDomain
    {
        public string OrderId { get; set; }
        public List<Refund> Refunds { get; set; }
    }
}