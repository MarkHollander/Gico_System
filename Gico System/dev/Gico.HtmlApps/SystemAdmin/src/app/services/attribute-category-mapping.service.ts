import { Injectable } from '@angular/core';
import { HttpClientService } from '../common/http-client.service';
import { ConfigSetting } from '../common/configSetting'
import { AttrCategoryAddRequest, AttrCategoryChangeRequest } from '../models/category-manager-model';
@Injectable()
export class AttributeCategoryMappingService {

  constructor(private httpClient: HttpClientService) { }


  async getAttrCategory(attrId:number,categoryId:string): Promise<any> {
  
    let request = {
      AttributeId : attrId,
       CategoryId : categoryId
    }

    let response = await this.httpClient.postJson(ConfigSetting.UrlPathAttrCategoryGet, request);
    let result = response.json() as any;
    return result;
  }
  async changeAttrCategory(model:AttrCategoryChangeRequest): Promise<any> {
    let request = {
      AttrCategory: model
    };
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathAttrCategoryChange, request);
    let result = response.json() as any;
    return result;
  }


  async removeCategoryAttr(attributeId:number,categoryId:string): Promise<any> {
    let request = {
      AttributeId:attributeId,
      CategoryId:categoryId
    };
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathAttrCategoryRemove, request);
    let result = response.json() as any;
    return result;
  }


}
