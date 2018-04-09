import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { ConfigSetting } from '../../../common/configSetting';
import { ResultModel, KeyValueModel } from '../../../models/result-model';
import { GiftcodeService } from '../../../services/giftcode.service';
import { GiftCodeCampaignGetResponseModel } from '../../../models/giftcodes/gift-code-campaign-get-response-model';
import { GiftCodeCampaignViewModel } from '../../../models/giftcodes/gift-code-campaign-view-model';
import { GiftCodeCampaignAddOrChangeRequestModel } from '../../../models/giftcodes/gift-code-campaign-add-or-change-request-model';
import { CalendarComponent } from '../calendar/calendar.component';
import { PromiseObservable } from 'rxjs/observable/PromiseObservable';
declare var App: any;
declare var jquery: any;
declare var $: any;
@Component({
  selector: 'app-gift-code-campaign-add-or-change',
  templateUrl: './gift-code-campaign-add-or-change.component.html',
  styleUrls: ['./gift-code-campaign-add-or-change.component.css']
})
export class GiftCodeCampaignAddOrChangeComponent implements OnInit {
  @ViewChild(CalendarComponent) calendar: CalendarComponent;
  private id: string;
  private shardId: number;
  private giftCodeCampaign: GiftCodeCampaignAddOrChangeRequestModel;
  private conditionTypes: KeyValueModel[];
  private listDomainEmail: String[];
  private DomainEmail: String;
  private listEmail: String[];
  constructor(
    private route: ActivatedRoute,
    private giftcodeService: GiftcodeService
  ) {
    this.route.params.subscribe(params => {
      this.id = params.id;
      this.shardId = params.shardid;
    });
  }

  ngOnInit() {
    this.onRegisterComponent();
    this.giftCodeCampaign = new GiftCodeCampaignAddOrChangeRequestModel();
    this.onInit();
    this.listDomainEmail = [];
  }
  async onRegisterComponent(): Promise<void> {
    $('.date-picker').datepicker({
      rtl: App.isRTL(),
      orientation: "left",
      autoclose: true
    })
  }
  async onMaxUsingCountByUserFocus(event: any): Promise<void> {
    if ($(event.target).hasClass("registed")) {
      return;
    }
    $(event.target).addClass("registed");
    $(event.target).inputmask({ mask: "9", repeat: 10, greedy: !1 });
  }
  async onInit(): Promise<void> {
    try {
      var response = await this.giftcodeService.campaignGet(this.id, this.shardId);
      var result = response as GiftCodeCampaignGetResponseModel;
      if (result.status) {
        let giftcodeCampaign = result.campaign;
        this.giftCodeCampaign.allowPaymentOnCheckout = giftcodeCampaign.allowPaymentOnCheckout;
        this.giftCodeCampaign.beginDate = giftcodeCampaign.beginDate;
        this.giftCodeCampaign.endDate = giftcodeCampaign.endDate;
        this.giftCodeCampaign.conditions = giftcodeCampaign.conditions;
        this.giftCodeCampaign.calendars = giftcodeCampaign.calendars;
        this.giftCodeCampaign.id = giftcodeCampaign.id;
        this.giftCodeCampaign.name = giftcodeCampaign.name;
        this.giftCodeCampaign.notes = giftcodeCampaign.notes;
        this.giftCodeCampaign.shardId = giftcodeCampaign.shardId;
        this.giftCodeCampaign.version = giftcodeCampaign.version;
        this.conditionTypes = result.conditionTypes;
        this.giftCodeCampaign.calendarMode = giftcodeCampaign.calendarMode;
      }
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
  async onSave(form): Promise<void> {
    App.blockUI();
    try {
      debugger;
      if (form.valid) {
        var response = await this.giftcodeService.campaignSave(this.giftCodeCampaign);
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
  async onShowCalendar(): Promise<void> {
    this.giftCodeCampaign.beginDate = $("#gift-code-campaign-add-or-change input[name='beginDate']").val();
    this.giftCodeCampaign.endDate = $("#gift-code-campaign-add-or-change input[name='endDate']").val();
    if (this.giftCodeCampaign.beginDate == null || this.giftCodeCampaign.beginDate == undefined || this.giftCodeCampaign.beginDate.length <= 0) {
      ConfigSetting.ShowError("beginDate");
      return;
    }
    if (this.giftCodeCampaign.endDate == null || this.giftCodeCampaign.endDate == undefined || this.giftCodeCampaign.endDate.length <= 0) {
      ConfigSetting.ShowError("endDate");
      return;
    }
    try {
      let beginDate = $("#gift-code-campaign-add-or-change input[name='beginDate']").datepicker('getDate');
      let endDate = $("#gift-code-campaign-add-or-change input[name='endDate']").datepicker('getDate');
      this.calendar.beginDate = beginDate;
      this.calendar.endDate = endDate.setDate(endDate.getDate() + 1);;
      $('#giftcode-calendar').modal('show');
      this.calendar.registerCalendar();
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
  async onClickCondition(value: string): Promise<void> {
    let condition = this.conditionTypes.find(x => x.value == value);
    condition.checked = !condition.checked;
  }
  async onAddDomainEmail(): Promise<void> {
    App.blockUI();
    let DomainEmails = this.DomainEmail.split(',');
    DomainEmails.forEach(element => {
      if (this.listDomainEmail.findIndex(x => x.valueOf() == element) < 0) {
        this.listDomainEmail.push(element);
      }

    });
    this.DomainEmail = "";
    App.unblockUI();
  }
  async onShowConfigProduct(): Promise<void> {
    $('#giftcode-configproduct').modal({
      backdrop: 'static'
    });
  }
  async onShowConfigCategory(): Promise<void> {
    $('#giftcode-configcategory').modal({
      backdrop: 'static'
    });
  }
  async onShowConfigVender(): Promise<void> {
    $('#giftcode-configvender').modal({
      backdrop: 'static'
    });
  }
  async onShowConfigEmail(): Promise<void> {
    $('#giftcode-configemail').modal({
      backdrop: 'static'
    });
  }
  async onShowConfigmobile(): Promise<void> {
    $('#giftcode-configmobile').modal({
      backdrop: 'static'
    });
  }
}
