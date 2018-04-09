using Gico.Config;
using Gico.Models.Response;
using Gico.OmsModels.Models;

namespace Gico.OmsModels.Response
{
    public class GiftCodeGroupGetsResponse : BaseResponse
    {
        public GiftCodeGroupGetsResponse()
        {
            GroupStatus = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GiftCodeGroupStatus));
        }
        public GiftCodeGroupViewModel[] GiftCodeGroups { get; set; }
        public KeyValueTypeIntModel[] GroupStatus { get; set; }
    }
}