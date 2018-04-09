import { ResultModel, KeyValueModel, BaseModel } from './result-model';

export class LocationUpdateRequest {
    id: number;
    provinceName: string;
    provinceNameEN: string;
    districtName: string;
    districtNameEN: string;
    provinceId: number;
    wardName: string;
    wardNameEN: string;
    districtId: number;
    streetName: string;
    streetNameEN: string;
    wardId: number;
    prefix: string;
    shortName: string;
    typeLocation: number;
}