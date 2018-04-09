using Gico.Config;

namespace Gico.ReadOrderModels.Giftcodes
{
    public class RGiftCodeCondition
    {
        public int Id { get; private set; }
        public int CampaignId { get; private set; }
        public EnumDefine.GiftCodeConditionTypeEnum ConditionType { get; private set; }
        public object Condition { get; private set; }
    }
}