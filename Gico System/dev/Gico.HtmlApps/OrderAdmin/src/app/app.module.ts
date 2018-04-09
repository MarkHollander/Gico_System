import { NgModule } from '@angular/core';

import { EqualValidator } from './directives/equal-validator.directive';

import { Convert } from './common/Convert';
import { ConfigSetting } from './common/configSetting';
import { HttpClientService } from './common/http-client.service'

import { CheckPermissionService } from './services/check-permission.service';
import { LoginRedirectService } from './services/login-redirect.service';
import { AccountService } from './services/account.service';
import { GiftcodeService } from './services/giftcode.service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';







@NgModule({
  declarations: [
    AppComponent,
    
  ],
  imports: [
    AppRoutingModule
  ],
  providers: [
    HttpClientService,
    CheckPermissionService,
    LoginRedirectService,
    AccountService,
    GiftcodeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
