import { Component, OnInit } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AccountService } from '../../services/account.service';
import { ConfigSetting } from '../../common/configSetting';

import { LoginModel } from '../../models/login-model';
import { promise } from 'selenium-webdriver';
declare var App: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: LoginModel;
  constructor(
    private router: Router,
    private accountService: AccountService
  ) { }

  ngOnInit() {
    this.model = new LoginModel();
    this.model.email = "a1@gmail.com";
    this.model.password = "123456";
  }

  async onInit(): Promise<void> {
    try {

    } catch (error) {
      ConfigSetting.ShowErrorException(error);
    }
  }

  async onLogin(loginForm): Promise<void> {
    App.blockUI();;
    try {
      console.log(loginForm);
      if (loginForm.valid) {
        let response = await this.accountService.login(this.model.email, this.model.password, this.model.remember);
        this.model.message = response.messages.join();
        if (response.status) {
          this.router.navigateByUrl(ConfigSetting.HomePage);
        }
      }

    } catch (error) {
      ConfigSetting.ShowErrorException(error);
    }
    App.unblockUI();
  }

}
