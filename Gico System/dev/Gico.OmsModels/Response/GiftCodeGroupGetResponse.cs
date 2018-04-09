using Gico.Config;
using Gico.Models.Response;
using Gico.OmsModels.Models;

namespace Gico.OmsModels.Response
{
    public class GiftCodeGroupGetResponse : BaseResponse
    {
        public GiftCodeGroupGetResponse()
        {
            GroupStatus = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GiftCodeGroupStatus));
        }
        public GiftCodeGroupViewModel Group { get; set; }
        public KeyValueTypeIntModel[] GroupStatus { get; set; }
    }
}