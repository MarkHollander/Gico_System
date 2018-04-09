using Gico.Domains;

namespace Gico.OrderDomains.Giftcodes
{
    public class GiftCode : BaseDomain
    {
        public string Prefix { get; private set; }
        public string CampaignId { get; private set; }
        public string GroupId { get; private set; }
        public string ImageUrl { get; private set; }
    }
}