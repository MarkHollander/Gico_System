using System;
using Gico.Domains;

namespace Gico.OrderDomains.Giftcodes
{
    public class GiftCodeGroup : BaseDomain
    {
        public string CampaignId { get;private set; }
        public string Name { get; private set; }
        public string Notes { get; private set; }
        public DateTime? ApprovedDate { get; private set; }
        public DateTime? ApprovedUid { get; private set; }
        public int Quantity { get; private set; }
        public int MaxUsingCount { get; private set; }
        public int FrequencyByUse { get; private set; }
        public decimal Value { get; private set; }
        public decimal ValueUsed { get; private set; }
        public int GiftCodeType { get; private set; }
        public bool IsApplyWithPromotion { get; private set; }
        public bool IsApplyMultiple { get; private set; }
        public string ImageUrl { get; private set; }
    }
}