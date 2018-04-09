import { GiftCodeCampaignViewModel } from './gift-code-campaign-view-model'
import { KeyValueModel, ResultModel } from '../result-model'

export class GiftCodeCampaignGetResponseModel extends ResultModel {
    campaign: GiftCodeCampaignViewModel;
    conditionTypes: KeyValueModel[];
}
