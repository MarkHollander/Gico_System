using Gico.Domains;

namespace Gico.OrderDomains
{
    public class PaymentDetail : BaseDomain
    {
        public string OrderItemId { get; set; }
        public string PaidAdvanceId { get; set; }
        public string PaidAdvanceCode { get; set; }
        public decimal Amount { get; set; }
        public int Priority { get; set; }
        
    }
}