
import { Injectable } from '@angular/core';
import { ConfigSetting } from '../common/configSetting'
import { HttpClientService } from '../common/http-client.service';
import { LocaleStringResourceSearchRequestModel } from '../models/locale-string-resource-search-request-model';
import { LocaleStringResourceModel } from '../models/locale-string-resource-model';

@Injectable()
export class LocaleStringResourceService {

  constructor(private httpClient: HttpClientService) { }
  
    async search(request:LocaleStringResourceSearchRequestModel): Promise<any> {      
      let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathLocaleSearch, request);
      let result = response.json() as any;
      return result;
    }
    async get(id:string): Promise<any> {      
      var request ={
        id
      };
      let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathCustoUrlPathLocaleGet, request);
      let result = response.json() as any;
      return result;
    }
    async save(locale: LocaleStringResourceModel): Promise<any> {
      var request = locale;
      let url = "";
      if (locale.id != undefined && locale.id != null && locale.id != "") {
        url = ConfigSetting.UrlPathLocaleChange;
      }
      else {
        url = ConfigSetting.UrlPathLocaleAdd;
      }
      let response = await this.httpClient.postJsonWithAuthen(url, request);
      let result = response.json() as any;
      return result;
    }
}
