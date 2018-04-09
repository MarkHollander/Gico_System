import { Component, OnInit } from '@angular/core';
import { LocationService } from '../../services/location.service';
import { LocationRequest } from '../../models/location-request-model';
import { LocationResponse } from '../../models/location-response-model';
import { LocationUpdateRequest } from '../../models/location-add-or-update-model';
import { ConfigSetting } from '../../common/configSetting';

declare var $: any;
declare var App: any;

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent implements OnInit {
  searchParams: LocationRequest;
  locationResponse: LocationResponse;
  locationUpdateRequest: LocationUpdateRequest;
  constructor(
    private locationService: LocationService,
  ) { }

  ngOnInit() {
    this.locationResponse = new LocationResponse();
    this.searchParams = new LocationRequest();
    this.locationUpdateRequest = new LocationUpdateRequest();
    this.registerMenusTree();
  }

  async registerMenusTree() {
    let response = await this.locationService.Index(this.searchParams);
    this.locationResponse.ltsProvince = response.ltsProvince;
    let menus = {
      'core': {
        'data': [],
        'check_callback': true
      }
    };
    for (let i = 0; i < this.locationResponse.ltsProvince.length; i++) {
      let item = this.locationResponse.ltsProvince[i];
      let provinceItem = {
        "id": 'provinceId_' + item.id,
        "text": item.provinceName,
        "data": item
      }
      menus.core.data.push(provinceItem);
    }
    try {
      $('#menus').jstree(true).settings.core.data = menus.core.data;
      $('#menus').jstree(true).refresh();
    }
    catch (ex) {
      $('#menus').jstree(menus);
    }

    var $that = this;
    $("#menus").on("select_node.jstree", function (event, node) {
      let typeNode = node.selected[0].split('_')[0];
      let idNode = node.selected[0].split('_')[1];
      switch (typeNode) {
        case "provinceId":
          $that.onGenerateDictrict(idNode);
          $that.onGetProvinceById(idNode);
          break;
        case "district":
          $that.onGenerateWard(idNode);
          $that.onGetDistrictById(idNode);
          break;
        case "ward":
          $that.onGenerateStreet(idNode);
          $that.onGetWardById(idNode);
          break;
        case "street":
          $that.onGetStreetById(idNode);
          break;
        default:
          break;
      }
    });
  }

  async onGenerateDictrict(provinceId: string) {
    this.searchParams.provinceId = provinceId;
    let response = await this.locationService.DistrictGetByProvinceId(this.searchParams);
    let data = response.ltsDictrics;
    for (let i = 0; i < data.length; i++) {
      $('#menus').jstree('create_node', $('#provinceId_' + provinceId), { 'id': 'district_' + data[i].id, 'text': data[i].districName }, 'last', false, false);
    }
    var obj = $('#menus').jstree(true).get_node('#provinceId_' + provinceId);
    $('#menus').jstree(true).open_node(obj);
    this.locationResponse.ltsDistricts = data;
  }

  async onGenerateWard(districId: string) {
    this.searchParams.districId = districId;
    let response = await this.locationService.GetWardByDistrictId(this.searchParams);
    let data = response.ltsWard;
    for (let i = 0; i < data.length; i++) {
      $('#menus').jstree('create_node', $('#district_' + districId), { 'id': 'ward_' + data[i].id, 'text': data[i].wardName }, 'last', false, false);
    }
    var obj = $('#menus').jstree(true).get_node('#district_' + districId);
    $('#menus').jstree(true).open_node(obj);
  }

  async onGenerateStreet(wardId: string) {
    this.searchParams.wardId = wardId;
    let response = await this.locationService.GetStreetByWardId(this.searchParams);
    let data = response.ltsStreet;
    for (let i = 0; i < data.length; i++) {
      $('#menus').jstree('create_node', $('#ward_' + wardId), { 'id': 'street_' + data[i].id, 'text': data[i].streetName }, 'last', false, false);
    }
    var obj = $('#menus').jstree(true).get_node('#ward_' + wardId);
    $('#menus').jstree(true).open_node(obj);
  }

  async onGetProvinceById(provinceId: string): Promise<void> {
    this.searchParams.provinceId = provinceId;
    const response = await this.locationService.GetProvinceById(this.searchParams);
    this.locationUpdateRequest.id = response.id;
    this.locationUpdateRequest.provinceName = response.provinceName;
    this.locationUpdateRequest.provinceNameEN = response.provinceNameEN;
    this.locationUpdateRequest.typeLocation = 1;
  }

  async onGetDistrictById(districId: string): Promise<void> {
    this.searchParams.districId = districId;
    const response = await this.locationService.GetDistrictById(this.searchParams);
    this.locationUpdateRequest.id = response.id;
    this.locationUpdateRequest.districtName = response.districtName;
    this.locationUpdateRequest.districtNameEN = response.districtNameEN;
    this.locationUpdateRequest.provinceId = response.provinceId;
    this.locationUpdateRequest.provinceName = response.provinceName;
    this.locationUpdateRequest.prefix = response.prefix;
    this.locationUpdateRequest.shortName = response.shortName;
    this.locationUpdateRequest.typeLocation = 2;
  }

  async onGetWardById(wardId: string): Promise<void> {
    this.searchParams.wardId = wardId;
    const response = await this.locationService.GetWardById(this.searchParams);
    this.locationUpdateRequest.id = response.id;
    this.locationUpdateRequest.wardName = response.wardName;
    this.locationUpdateRequest.wardNameEN = response.wardNameEN;
    this.locationUpdateRequest.prefix = response.prefix;
    this.locationUpdateRequest.shortName = response.shortName;
    this.locationUpdateRequest.provinceId = response.provinceId;
    this.locationUpdateRequest.provinceName = response.provinceName;
    this.locationUpdateRequest.districtId = response.districtId;
    this.locationUpdateRequest.districtName = response.districtName;
    this.locationUpdateRequest.typeLocation = 3;
  }

  async onGetStreetById(streetId: string): Promise<void> {
    this.searchParams.streetId = streetId;
    const response = await this.locationService.GetStreetById(this.searchParams);
    this.locationUpdateRequest.id = response.id;
    this.locationUpdateRequest.streetName = response.streetName;
    this.locationUpdateRequest.streetNameEN = response.streetNameEN;
    this.locationUpdateRequest.typeLocation = 4;
  }

  onChangeProvince(provinceId){
    console.log(provinceId); 
    console.log(provinceId.target.value);
    console.log(provinceId.target.options[provinceId.target.selectedIndex].text); 
    console.log(provinceId.text); 
    console.log(provinceId.value); 
 }
  
  async onUpdate(form): Promise<void> {
    App.blockUI();
    try {
      if (form.valid) {
        const request = this.locationUpdateRequest;
        const response = await this.locationService.UpdateProvince(request);
        if (response.status) {
          ConfigSetting.ShowSuccess('Save sucess.');
          this.registerMenusTree();
        } else {
          ConfigSetting.ShowErrores(response.messages);
        }
      }
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }

  async onDistrictUpdate(form): Promise<void> {
    App.blockUI();
    try {
      if (form.valid) {
        const request = this.locationUpdateRequest;
        const response = await this.locationService.UpdateDistrict(request);
        if (response.status) {
          ConfigSetting.ShowSuccess('Save sucess.');
          this.registerMenusTree();
        } else {
          ConfigSetting.ShowErrores(response.messages);
        }
      }
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }

  async onWardUpdate(form): Promise<void> {
    App.blockUI();
    try {
      if (form.valid) {
        const request = this.locationUpdateRequest;
        const response = await this.locationService.UpdateWard(request);
        if (response.status) {
          ConfigSetting.ShowSuccess('Save sucess.');
          this.registerMenusTree();
        } else {
          ConfigSetting.ShowErrores(response.messages);
        }
      }
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }

  async onDelete(id: string, typeLocation: string): Promise<void>{
    App.blockUI();
    try {
      switch (typeLocation) {
        case "province":
          this.searchParams.provinceId = id;
          this.searchParams.typeLocation = 1;
          break;
        case "district":
          this.searchParams.districId = id;
          this.searchParams.typeLocation = 2;
          break;
        case "ward":
          this.searchParams.wardId = id;
          this.searchParams.typeLocation = 3;
          break;
        case "street":
          this.searchParams.streetId = id;
          this.searchParams.typeLocation = 4;
          break;
        default:
          break;
      }
      const response = await this.locationService.Delete(this.searchParams);
      ConfigSetting.ShowSuccess('Delete sucess.');
    } catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }
}
