using Gico.OmsModels.Response;
using System.Threading.Tasks;
using Gico.Models.Response;
using Gico.OmsModels.Request;

namespace Gico.OmsAppService.Interfaces
{
    public interface IGiftcodeAppService
    {
        Task<GiftCodeCampaignGetsResponse> GiftCodeCampaignGet(GiftCodeCampaignGetsRequest request);
        Task<GiftCodeCampaignGetResponse> GiftCodeCampaignGet(GiftCodeCampaignGetRequest request);
        Task<BaseResponse> GiftCodeCampaignAddOrChange(GiftCodeCampaignAddOrChangeRequest request);
        Task<BaseResponse> GiftCodeCampaignChangeStatus(GiftCodeCampaignChangeStatusRequest request);
        Task<GiftCodeGroupGetsResponse> Get(GiftCodeGroupGetsRequest request);
        Task<GiftCodeGroupGetResponse> Get(GiftCodeGroupGetRequest request);
    }
}