import { Injectable } from '@angular/core';
import { ConfigSetting } from '../common/configSetting'
import { HttpClientService } from '../common/http-client.service';
import {LocationRequest} from '../models/location-request-model';
import { LocationUpdateRequest } from '../models/location-add-or-update-model';

@Injectable()
export class LocationService {

  constructor(private httpClient: HttpClientService) { }

  async Index(request:LocationRequest): Promise<any> {      
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathLocation, request);
    let result = response.json() as any;
    return result;
  }

  async DistrictGetByProvinceId(request:LocationRequest): Promise<any> {
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetDictrictByProvinceId, request);
    let result = response.json() as any;
    return result;
  }

  async GetWardByDistrictId(request: LocationRequest): Promise<any>{
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetWardByDictrictId, request);
    let result = response.json() as any;
    return result;
  }

  async GetStreetByWardId(request: LocationRequest): Promise<any>{
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetStreetByWardId, request);
    let result = response.json() as any;
    return result;
  }

  async GetProvinceById(request: LocationRequest): Promise<any> {
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetProvinceById, request);
    let result = response.json() as any;
    return result;
  }

  async GetDistrictById(request: LocationRequest): Promise<any> {
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetDistrictById, request);
    let result = response.json() as any;
    return result;
  }

  async GetWardById(request: LocationRequest): Promise<any> {
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetWardById, request);
    let result = response.json() as any;
    return result;
  }

  async GetStreetById(request: LocationRequest): Promise<any> {
    let response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathGetStreetById, request);
    let result = response.json() as any;
    return result;
  }

  async UpdateProvince(request: LocationUpdateRequest): Promise<any> {
    const response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathProvinceUpdate, request);
    const result = response.json() as any;
    return result;
  }

  async UpdateDistrict(request: LocationUpdateRequest): Promise<any> {
    const response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathDistrictUpdate, request);
    const result = response.json() as any;
    return result;
  }

  async UpdateWard(request: LocationUpdateRequest): Promise<any> {
    const response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathWardUpdate, request);
    const result = response.json() as any;
    return result;
  }

  async Delete(request: LocationRequest): Promise<any> {
    const response = await this.httpClient.postJsonWithAuthen(ConfigSetting.UrlPathLocationDelete, request);
    const result = response.json() as any;
    return result;
  }
}
