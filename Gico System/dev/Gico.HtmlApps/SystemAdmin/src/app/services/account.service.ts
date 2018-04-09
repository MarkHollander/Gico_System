import { Injectable } from '@angular/core';

import { ConfigSetting } from '../common/configSetting'
import { HttpClientService } from '../common/http-client.service';

import { ResultModel } from '../models/result-model';
import { Dictionary } from '../models/dictionary';

@Injectable()
export class AccountService {

  constructor(private httpClient: HttpClientService) { }

  async login(email: string, password: string, remember: boolean): Promise<any> {
    let request = {
      email,
      password,
      remember
    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathLogin, request);
    let result = response.json() as any;
    if (result.status) {
      let actionIds = new Dictionary<boolean>();
      if (!result.isAdministrator) {
        if (result.actionIds != null && result.actionIds != undefined && result.actionIds.length > 0) {
          for (let i = 0; i < result.actionIds.length; i++) {
            actionIds.Add(result.actionIds[i], true);
          }
        }
      }
      ConfigSetting.SetLoginStatus(result.tokenKey, result.isAdministrator, actionIds);
      ConfigSetting.ShowSuccess("Login success.");
    }
    else {
      ConfigSetting.ShowErrores(result.messages);
    }
    return result;
  }
  async register(fullName: string, email: string, password: string, confirmPassword: string): Promise<any> {
    let request = {
      fullName,
      email,
      password,
      confirmPassword
    }
    let response = await this.httpClient.postJson(ConfigSetting.UrlPathRegister, request);
    let result = response.json() as any;
    if (result.status) {
      ConfigSetting.SetLoginStatus(result.TokenKey, null, null);
      ConfigSetting.ShowSuccess("Register success.");
    }
    else {
      ConfigSetting.ShowErrores(result.messages);
    }
    return result;
  }
}
