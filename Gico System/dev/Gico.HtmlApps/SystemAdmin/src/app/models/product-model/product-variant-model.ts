import { KeyValueModel } from '../result-model';

export class ProductVariantModel {
    typeId: number;
    merchantTypeId: number;
    vendorId: number;
    warehouseId: number;
    unitNameMerchant: number;
    vat: number;
    vatExt: number;
    attributeVariants: KeyValueModel[];
}
