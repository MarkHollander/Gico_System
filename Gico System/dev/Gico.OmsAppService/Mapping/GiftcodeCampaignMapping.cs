using Gico.OmsModels.Models;
using Gico.OmsModels.Request;
using Gico.OrderCommands.Giftcodes;
using Gico.OrderDomains.Giftcodes;
using Gico.ReadOrderModels.Giftcodes;

namespace Gico.OmsAppService.Mapping
{
    public static class GiftcodeCampaignMapping
    {
        public static GiftCodeCampaignViewModel ToModel(this RGiftCodeCampaign campaign)
        {
            if (campaign == null)
            {
                return null;
            }
            return new GiftCodeCampaignViewModel()
            {

            };
        }

        public static GiftCodeCampaignAddCommand ToCommand(this GiftCodeCampaignAddOrChangeRequest request, string currentUid)
        {
            if (request == null)
            {
                return null;
            }
            return new GiftCodeCampaignAddCommand()
            {

            };
        }
        public static GiftCodeCampaignChangeCommand ToCommand(this GiftCodeCampaignAddOrChangeRequest request, string currentUid, int version, int shardId)
        {
            if (request == null)
            {
                return null;
            }
            return new GiftCodeCampaignChangeCommand()
            {

            };
        }
        public static GiftCodeCampaignChangeStatusCommand ToCommand(this GiftCodeCampaignChangeStatusRequest request, string currentUid, int version, int shardId)
        {
            if (request == null)
            {
                return null;
            }
            return new GiftCodeCampaignChangeStatusCommand()
            {

            };
        }
    }
}