import { Injectable } from '@angular/core';
import { ConfigSetting } from '../common/configSetting'
import { HttpClientService } from '../common/http-client.service';
import { WarehouseSearchRequest } from '../models/warehouse/warehouse-search-request';

@Injectable()
export class WarehouseService {

  constructor(private httpClient: HttpClientService) { }
  
  async search(request:WarehouseSearchRequest): Promise<any> {      
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathWarehouseSearch, request);
    let result = response.json() as any;
    return result;
  }

  async get(id:string): Promise<any> {      
    var request ={
      id
    };
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathWarehouseGet, request);
    let result = response.json() as any;
    return result;
  }


}
