using System;
using Gico.Config;

namespace Gico.FrontEndModels.Models
{
    public class PaidAdvanceViewModel
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public EnumDefine.PiadAdvanceTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public string TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime TransactionDate { get; set; }
  
    }
}