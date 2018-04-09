using System;
using Gico.Domains;

namespace Gico.OrderDomains.Giftcodes
{
    public class GiftCodeUsed : BaseDomain
    {
        public decimal Value { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Uid { get; set; }
        public string SOID { get; set; }
        public string CampaignId { get; set; }
        public string GroupId { get; set; }
    }
}