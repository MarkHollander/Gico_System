import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ConfigSetting } from '../common/configSetting';

@Injectable()
export class CheckPermissionService implements CanActivate {
  constructor(private router: Router) { }
  canActivate(): boolean {
    if (ConfigSetting.GetLoginStatus()) {
      return true;
    }
    else {
      this.router.navigateByUrl(ConfigSetting.LoginPage);
      return false;
    }
  }
}