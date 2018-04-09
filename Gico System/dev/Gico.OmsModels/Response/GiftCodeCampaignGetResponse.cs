using Gico.Config;
using Gico.Models.Response;
using Gico.OmsModels.Models;

namespace Gico.OmsModels.Response
{
    public class GiftCodeCampaignGetResponse : BaseResponse
    {
        public GiftCodeCampaignGetResponse()
        {
            ConditionTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GiftCodeConditionTypeEnum),false);
        }
        public GiftCodeCampaignViewModel Campaign { get; set; }
        public KeyValueTypeIntModel[] ConditionTypes { get; set; }
    }
}