import { Injectable } from '@angular/core';
import { ConfigSetting } from '../common/configSetting'
import { HttpClientService } from '../common/http-client.service';

import { ResultModel } from '../models/result-model';
import { Dictionary } from '../models/dictionary';
import { GiftCodeCampaignAddOrChangeRequestModel } from '../models/giftcodes/gift-code-campaign-add-or-change-request-model'

@Injectable()
export class GiftcodeService {

  constructor(private httpClient: HttpClientService) { }

  async campaignGet(id: string, shardId: number): Promise<any> {
    let request = {
      id,
      shardId
    }
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGiftcodeCampaignGet, request);
    let result = response.json() as any;    
    return result;
  }
  async campaignSave(campaign:GiftCodeCampaignAddOrChangeRequestModel): Promise<any> {
    let request = campaign;
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGiftcodeCampaignAdd, request);
    let result = response.json() as any;    
    return result;
  }
}
