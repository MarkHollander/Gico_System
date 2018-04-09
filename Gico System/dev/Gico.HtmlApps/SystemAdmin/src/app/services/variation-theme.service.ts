import { Injectable } from '@angular/core';
import { HttpClientService } from '../common/http-client.service';
import { ConfigSetting } from '../common/configSetting'
import {VariationThemeModel, Category_VariationTheme_AddRequest}  from '../models/variation-theme-model';
@Injectable()
export class VariationThemeService {

  constructor(private httpClient: HttpClientService) { }

  async getGetVariationTheme(): Promise<any> {
    let request = {

    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathVariationThemeGet,request);
    let result = response.json() as any;
    return result;
  }


  async getGetVariationTheme_Attribute(variationThemeId:number): Promise<any> {
    let request = {
      id:variationThemeId
     
    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathVariationThemeAttributeGet,request);
    let result = response.json() as any;
    return result;
  }

  async add(category_VariationTheme:Category_VariationTheme_AddRequest): Promise<any> {
    let request = {
      category_VariationTheme_Mapping:category_VariationTheme
    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathCategoryVariationThemeMappingAdd,request);
    let result = response.json() as any;
    return result;
  }

  async getsCategoryVariationTheme(categoryId:string): Promise<any> {
    let request = {
        categoryId:categoryId
    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathCategoryVariationThemeMappingGets,request);
    let result = response.json() as any;
    return result;
  }

  async remove(categoryId:string,variationThemeId:number): Promise<any> {
    let request = {
        categoryId:categoryId,
        variationThemeId:variationThemeId
    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathCategoryVariationThemeMappingRemove,request);
    let result = response.json() as any;
    return result;
  }

}
