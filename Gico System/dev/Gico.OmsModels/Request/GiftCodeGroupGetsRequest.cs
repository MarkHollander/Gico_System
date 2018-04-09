using Gico.Config;

namespace Gico.OmsModels.Request
{
    public class GiftCodeGroupGetsRequest
    {
        public string CampaignId { get; set; }
        public string Name { get; set; }
        public EnumDefine.GiftCodeGroupStatus Status { get; set; }
    }
}