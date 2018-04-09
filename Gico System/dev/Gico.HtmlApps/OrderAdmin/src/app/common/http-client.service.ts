import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { Router } from '@angular/router';

import { ConfigSetting } from './configSetting';

@Injectable()
export class HttpClientService {

  constructor(
    private http: Http,
    private router: Router,
  ) { }

  cantNotConnectObject = {
    json: function () {
      return {
        status: false,
        messages: ["Can't connect to server"]
      };
    }
  };

  async postJson(absolutePath: string, obj): Promise<any> {
    let url: string = ConfigSetting.CreateUrl(absolutePath);
    var headers = ConfigSetting.Headers;
    try {
      let response = await this.http.post(url, obj, { headers: headers }).toPromise();
      return response;
    } catch (error) {
      ConfigSetting.ShowError("Can't connect to server");
    }
    return this.cantNotConnectObject;
  }
  async postJsonWithAuthen(absolutePath: string, obj): Promise<any> {
    let url: string = ConfigSetting.CreateUrl(absolutePath);
    let token: string = ConfigSetting.GetAuthenToken;
    var headers = ConfigSetting.Headers;
    headers.set('Authorization', `Bearer ${token}`)
    let response = await this.http.post(url, obj, { headers: headers }).toPromise();
    if (response.status == 401) {
      ConfigSetting.Logout();
      return this.router.navigateByUrl(ConfigSetting.LoginPage);
    }
    else {
      let result = response.json();
      return response;
    }
  }
  async postJsonWithAuthenAndHeaders(absolutePath: string, obj, headers): Promise<any> {
    let url: string = ConfigSetting.CreateUrl(absolutePath);
    let token: string = ConfigSetting.GetAuthenToken;
    headers.set('Authorization', `Bearer ${token}`)
    let response = await this.http.post(url, obj, { headers: headers }).toPromise();
    if (response.status == 401) {
      ConfigSetting.Logout();
      return this.router.navigateByUrl(ConfigSetting.LoginPage);
    }
    else {
      let result = response.json();
      return response;
    }
  }
}
