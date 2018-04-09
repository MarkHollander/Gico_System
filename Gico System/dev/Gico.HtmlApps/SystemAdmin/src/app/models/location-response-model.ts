export class LocationResponse {
    ltsProvince: ProvinceReponse[];
    ltsDistricts: DistrictReponse[];
}

export class ProvinceReponse {
    provinceName: string;
    id: string;
}

export class DistrictReponse {
    districName: string;
    id: string;
}