using Gico.Config;

namespace Gico.OmsModels.Request
{
    public class GiftCodeCampaignChangeStatusRequest
    {
        public string Id { get; set; }
        public EnumDefine.GiftCodeCampaignStatus Status { get; set; }
        public int ShardId { get; set; }
        public int Version { get; set; }
    }
}