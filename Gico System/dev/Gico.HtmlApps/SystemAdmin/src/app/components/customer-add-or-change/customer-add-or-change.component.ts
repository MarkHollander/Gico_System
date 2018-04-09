import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ConfigSetting } from '../../common/configSetting';
import { CustomerAddOrChangeModel } from '../../models/customer-add-or-change-model';
import { CustomerService } from '../../services/customer.service';
import { KeyValueModel } from '../../models/result-model';
import { promise } from 'selenium-webdriver';
import { Jsonp } from '@angular/http/src/http';
declare var App: any;
declare var jQuery: any;
declare var $: any;

@Component({
  selector: 'app-customer-add-or-change',
  templateUrl: './customer-add-or-change.component.html',
  styleUrls: ['./customer-add-or-change.component.css']
})
export class CustomerAddOrChangeComponent implements OnInit {
  customer: CustomerAddOrChangeModel;
  types: KeyValueModel[];
  statuses: KeyValueModel[];
  genders: KeyValueModel[];
  languages: KeyValueModel[];
  twoFactorEnableds: KeyValueModel[];
  submited: boolean;
  _type:Int8Array[1];
  _typeDirty:boolean;
  @ViewChild('customerAddOrChange') form1: any;
  constructor(
    private customerService: CustomerService
  ) { }

  ngOnInit() {
    this.customer = new CustomerAddOrChangeModel();
    this.submited = false;
    this._type=0;
    if (jQuery().datepicker) {
      $('.date-picker').datepicker({
          rtl: App.isRTL(),
          orientation: "left",
          autoclose: true
      });
      //$('body').removeClass("modal-open"); // fix bug when inline picker is used in modal
  }
  }
  async onGet(): Promise<void> {
    App.blockUI();;
    try {
      var response = await this.customerService.get(this.customer.id);
      this.customer = response.customer;
      this.types = response.types;
      this.statuses = response.statuses;
      this.genders = response.genders;
      this.languages = response.languages;
      this.twoFactorEnableds = response.twoFactorEnableds;
      if(this.customer.id!='')this._type=response.customer.type;
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }
  trackByIndex(index: number, obj: any): any {
    return index;
  }

  async onAddOrChange(form): Promise<void> {
    App.blockUI();
    let type = 0;
    this.types.forEach(element => {
      if (element.checked) {
       type += parseInt(element.value);
      }
    });
    this.customer.type = type;
    this.customer.birthday = $("#customer-add-or-change input[name='birthday']").val();
    this.submited = true;
    try {
      if (form.valid && type != 0) {
        ConfigSetting.ShowErrores(["success"]);
        let requestModel = this.customer;
        let response = await this.customerService.save(requestModel);
        if (response.status) {
          ConfigSetting.ShowSuccess("Save sucess.");
        }
        else {
          ConfigSetting.ShowErrores(response.messages);
        }
      }
    }

    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }
checkedType(event) {
  this._typeDirty=true;
    if(event.checked){
      this._type -= parseInt(event.value);
    }
    else {
      this._type += parseInt(event.value);
    }
  }
}
