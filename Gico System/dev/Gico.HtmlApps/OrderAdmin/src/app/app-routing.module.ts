import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { EqualValidator } from './directives/equal-validator.directive';

import { CheckPermissionService } from './services/check-permission.service';
import { LoginRedirectService } from './services/login-redirect.service';

import { BaseComponent } from './components/base/base.component';
import { LayoutComponent } from './components/layout/layout.component';
import { LoginComponent } from "./components/login/login.component";
import { HomeComponent } from './components/home/home.component';
import { GiftcodeCampaignComponent } from './components/giftcodes/giftcode-campaign/giftcode-campaign.component';
import { GiftCodeCampaignAddOrChangeComponent } from './components/giftcodes/gift-code-campaign-add-or-change/gift-code-campaign-add-or-change.component';
import { CalendarComponent } from './components/giftcodes/calendar/calendar.component';
import { CalendarAddTimeComponent } from './components/giftcodes/calendar-add-time/calendar-add-time.component';
import { ConfigProductComponent } from './components/giftcodes/config-product/config-product.component';
import { ConfigCategoryComponent } from './components/giftcodes/config-category/config-category.component';
import { ConfigPriceComponent } from './components/giftcodes/config-price/config-price.component';
import { ConfigVenderComponent } from './components/giftcodes/config-vender/config-vender.component';
import { ConfigEmailComponent } from './components/giftcodes/config-email/config-email.component';
import { ConfigMobileComponent } from './components/giftcodes/config-mobile/config-mobile.component';
import { ConfigPaymentTypeComponent } from './components/giftcodes/config-payment-type/config-payment-type.component';
import { ConfigDeviceTypeComponent } from './components/giftcodes/config-device-type/config-device-type.component';
import { ConfigProvinceComponent } from './components/giftcodes/config-province/config-province.component';

import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
const routesConfig: Routes = [
  {
    path: '',
    redirectTo: "login",
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [LoginRedirectService]
  }, 
  {
    path: 'g',
    component: LayoutComponent,
    canActivate: [CheckPermissionService],
    children: [
      {
        path: 'home',
        component: HomeComponent,
        canActivate: [CheckPermissionService]
      },
      {
        path: 'giftcodecampaign',
        component: GiftcodeCampaignComponent,
        canActivate: [CheckPermissionService]
      },
      {
        path: 'giftcodecampaignadd',
        component: GiftCodeCampaignAddOrChangeComponent,
        canActivate: [CheckPermissionService]
      },
      {
        path: 'giftcodecampaignchange:id',
        component: GiftCodeCampaignAddOrChangeComponent,
        canActivate: [CheckPermissionService]
      }
    ]
  },

  { path: '**', component: HomeComponent }
]

@NgModule({
  declarations: [
    EqualValidator,    
    BaseComponent,
    LayoutComponent,
    LoginComponent,
    HomeComponent,    
    FileUploadComponent,   
    GiftcodeCampaignComponent,
    GiftCodeCampaignAddOrChangeComponent,
    CalendarComponent,
    CalendarAddTimeComponent,
    ConfigProductComponent,
    ConfigCategoryComponent,
    ConfigPriceComponent,
    ConfigVenderComponent,
    ConfigEmailComponent,
    ConfigMobileComponent,
    ConfigPaymentTypeComponent,
    ConfigDeviceTypeComponent,
    ConfigProvinceComponent    
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(routesConfig),
    NgxPaginationModule,
    NgbModule.forRoot()
  ],
  exports: [
    RouterModule,
    EqualValidator
  ]
})

export class AppRoutingModule {

}