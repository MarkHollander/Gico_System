import { Injectable } from '@angular/core';
import { ConfigSetting } from '../common/configSetting'
import { HttpClientService } from '../common/http-client.service';
import { MenuModel } from '../models/menu-manager-model';
@Injectable()
export class MenuService {

  constructor(private httpClient: HttpClientService) { }

  async gets(languageId: string, position: number): Promise<any> {
    let request = {
      languageId,
      position
    };
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathMenuGets, request);
    let result = response.json() as any;
    return result;
  }

  async get(languageId: string, id: string, position: number): Promise<any> {
    let request = {
      id,
      languageId,
      position
    };
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathMenuGet, request);
    let result = response.json() as any;
    return result;
  }

  async addOrChange(model: MenuModel): Promise<any> {
    let request = {
      menu: model
    };
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathMenuAddOrChange, request);
    let result = response.json() as any;
    return result;
  }

  async getBannersByMenuId(menuId: string): Promise<any> {
    let request = {
      menuId
    }
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathMenuBannerMappingGet, request);
    let result = response.json() as any;
    return result;
  }

  async addBanner(menuId: string, bannerId: string): Promise<any> {
    let request = {
      menuId, bannerId
    }
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathMenuBannerMappingAdd, request);
    let result = response.json() as any;
    return result;
  }
  async removeBanner(menuId: string, bannerId: string): Promise<any> {
    let request = {
      menuId, bannerId
    }
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathMenuBannerMappingRemove, request);
    let result = response.json() as any;
    return result;
  }
}
