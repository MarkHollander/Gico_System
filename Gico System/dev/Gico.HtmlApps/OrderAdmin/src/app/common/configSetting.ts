
import { Headers } from '@angular/http';
import { Convert } from './convert'
import { Dictionary } from '../models/dictionary';
import { forEach } from '@angular/router/src/utils/collection';
import { retry } from 'rxjs/operators/retry';

declare var jquery: any;
declare var $: any;

export class ConfigSetting {
  public static BASE_URL: string = 'http://localhost:53131/api/';
  public static Headers: Headers = new Headers({ 'Content-Type': 'application/json' });

  public static UrlPathLogin: string = 'Account/GenerateToken';
  public static UrlPathFileUpload: string = 'File/Index';
  //#region giftcode
  public static UrlPathGiftcodeCampaignGet: string = 'Giftcode/Campaign';
  public static UrlPathGiftcodeCampaignAdd: string = 'Giftcode/CampaignAdd';
  //#endregion

  public static LoginExpiretime: number = 30;

  private static LocalStorageAuthenKey: string = "LocalStorageAuthenKey";
  public static CreateUrl(absolutePath: string): string {
    return `${ConfigSetting.BASE_URL}${absolutePath}`;
  }

  public static set SetAuthenToken(token: string) {
    localStorage.setItem(this.LocalStorageAuthenKey, token);
  }
  public static get GetAuthenToken(): string {
    return localStorage.getItem(this.LocalStorageAuthenKey);
  }

  public static LoginStatus: string = "LoginStatus";

  public static HomePage: string = "/g/home";
  public static LoginPage: string = '/login';
  public static CustomerDetailPage: string = '/g/DetailCustomer/';

  public static SetLoginStatus(authenToken: string, isAdministrator: boolean, actionIds: Dictionary<boolean>): void {
    var currentDate = new Date();
    currentDate.setMinutes(currentDate.getMinutes() + ConfigSetting.LoginExpiretime);
    var loginObject = {
      status: true,
      loginTime: currentDate.getTime(),
      isAdministrator: isAdministrator,
      actionIds: actionIds
    };
    var tmp = JSON.stringify(loginObject);
    localStorage.setItem(this.LoginStatus, tmp);
    ConfigSetting.SetAuthenToken = authenToken;
  }
  public static GetLoginStatus(): boolean {
    var tmp = localStorage.getItem(this.LoginStatus);
    if (tmp == null) {
      return false;
    }
    var loginObject = JSON.parse(tmp);
    if (loginObject == null || loginObject == undefined) {
      return false;
    }
    if (loginObject.status) {
      try {
        var currentDate = new Date();
        if (loginObject.loginTime < currentDate.getTime()) {
          return false;
        }
      }
      catch (ex) {
        return false;
      }
      return true;
    }
    else {
      return false;
    }
  }
  public static CheckPermission(actionIds: string[]): Dictionary<boolean> {
    var tmp = localStorage.getItem(this.LoginStatus);
    if (tmp == null) {
      return null;
    }
    var loginObject = JSON.parse(tmp);
    if (loginObject == null || loginObject == undefined) {
      return null;
    }
    if (loginObject.status) {
      try {
        var currentDate = new Date();
        if (loginObject.loginTime < currentDate.getTime()) {
          return null;
        }
        var permissions = new Dictionary<boolean>();
        if (loginObject.isAdministrator) {
          for (let i = 0; i < actionIds.length; i++) {
            permissions.Add(actionIds[i], true);
          }
          return permissions;
        }
        if (loginObject.actionIds == null || loginObject.actionIds == undefined || loginObject.actionIds.length <= 0) {
          return permissions;
        }
        for (let i = 0; i < actionIds.length; i++) {
          var isPermission = loginObject.actionIds.Item(actionIds[i]);
          permissions.Add(actionIds[i], isPermission == true);
        }
      }
      catch (ex) {
        return null;
      }
      return null;
    }
    else {
      return null;
    }
  }

  public static Logout() {
    localStorage.removeItem(this.LoginStatus);
    localStorage.removeItem(this.LocalStorageAuthenKey);
  }

  public static ShowError(message: string) {
    $.notify({
      message: message
    }, {
        type: 'danger'
      });
  }
  public static ShowErrores(messages: string[]) {
    let message: string = messages.join();
    $.notify({
      message: message
    }, {
        type: 'danger'
      });
  }
  public static ShowErrorException(error: any) {
    let message = "Lỗi không xác định";
    ConfigSetting.ShowError(message + ": " + error.message);
  }

  public static ShowSuccess(message: string) {
    $.notify({
      message: message
    }, {
        type: 'success'
      });
  }
}
