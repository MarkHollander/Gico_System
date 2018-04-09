using Gico.Config;
using Gico.Models.Response;
using Gico.OmsModels.Models;

namespace Gico.OmsModels.Response
{
    public class GiftCodeCampaignGetsResponse : BaseResponse
    {
        public GiftCodeCampaignGetsResponse()
        {
            CampaignStatus = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.GiftCodeCampaignStatus));
        }
        public GiftCodeCampaignViewModel[] Campaigns { get; set; }
        public KeyValueTypeIntModel[] CampaignStatus { get; set; }
      
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}