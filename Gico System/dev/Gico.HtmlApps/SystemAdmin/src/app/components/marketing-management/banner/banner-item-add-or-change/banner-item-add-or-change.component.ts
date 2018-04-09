import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { promise } from 'selenium-webdriver';
import { forEach } from '@angular/router/src/utils/collection';
import { Jsonp } from '@angular/http/src/http';
import { ConfigSetting } from '../../../../common/configSetting';
import { KeyValueModel } from '../../../../models/result-model';
import { BannerService } from '../../../../services/marketing-management/banner/banner.service';
import { Router } from '@angular/router';
import { BannerItem } from '../../../../models/marketing-management/banner/banner-item/banner-item';
import { Banner } from '../../../../models/marketing-management/banner/banner/banner';

declare var App: any;
declare var jQuery: any;
declare var $: any;

@Component({
  selector: 'app-banner-item-add-or-change',
  templateUrl: './banner-item-add-or-change.component.html',
  styleUrls: ['./banner-item-add-or-change.component.css']
})
export class BannerItemAddOrChangeComponent implements OnInit {
  @Input() bannerItemId: string;
  bannerItem: BannerItem;
  banner: Banner;
  statuses: KeyValueModel[];
  submited: boolean;

  constructor(private bannerService: BannerService,
    private router: Router) { }

  ngOnInit() {
    this.bannerItem = new BannerItem();
    this.banner = new Banner();
    this.submited = false;
    if (jQuery().datetimepicker) {
      $('.datetimepicker1').datetimepicker();
    }
  }
  async onGetDetail(): Promise<void> {
    App.blockUI();
    try {
      if (this.bannerItem.id != null && this.bannerItem.id !== '' && this.bannerItem.id !== undefined) {
        const response = await this.bannerService.getBannerItemById(this.bannerItem.id, '');
        this.bannerItem = response.bannerItem;
        this.banner = response.banner;
        this.statuses = response.statuses;
      } else {
        const response = await this.bannerService.getBannerById(this.bannerItem.bannerId);
        this.banner = response.banner;
        this.bannerItem = new BannerItem();
        this.bannerItem.isDefault = true;
        this.bannerItem.bannerId = this.banner.id;
        this.submited = false;

        console.log(this.bannerItem);
      }
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }

  async onAddOrChange(form): Promise<void> {
    App.blockUI();
    this.submited = true;
    try {
      if (form.valid) {
        const requestModel = this.bannerItem;
        const response = await this.bannerService.saveBannerItem(requestModel);
        if (response.status) {
          $('#banner-item-add-or-change').modal('hide');
          ConfigSetting.ShowSuccess('Save sucess.');
        } else {
          ConfigSetting.ShowErrores(response.messages);
        }
      }
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }
  async onChangeImage(): Promise<void> {
    App.blockUI();
    try {
      $('#file-uploader-popup').modal('show');
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }

  async getUploadedFile($event): Promise<void> {
    try {
      this.bannerItem.imageUrl = ConfigSetting.CDN_URL + $event;
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }
  async onSelectedCheckbox(): Promise<void> {
    try {
      $('input[name=\'startDate\']').val('');
      $('input[name=\'endDate\']').val('');
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
}

