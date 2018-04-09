using Gico.Config;
using Gico.Models.Response;
using Gico.OmsModels.Models;

namespace Gico.OmsModels.Response
{
    public class GiftcodeGetResponse
    {
        public GiftcodeGetResponse()
        {
            ConditionTypes = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GiftCodeConditionTypeEnum));
        }
        public GiftcodeViewModel Giftcode { get; set; }
        public KeyValueTypeIntModel[] ConditionTypes { get; set; }
    }
}