import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ProductContentModel } from '../../../../models/product-model/product-content-model';
import { ProductAttributeInfoModel } from '../../../../models/product-model/product-attribute-info-model';

declare var jquery: any;
declare var $: any;
declare var App: any;

@Component({
  selector: 'app-product-attribute-info',
  templateUrl: './product-attribute-info.component.html',
  styleUrls: ['./product-attribute-info.component.css']
})
export class ProductAttributeInfoComponent implements OnInit {
  @Output() setTab = new EventEmitter<string>();

  productAttributeInfo: ProductAttributeInfoModel;
  formValid = true;

  constructor() { }

  ngOnInit() {
    this.productAttributeInfo = new ProductAttributeInfoModel();
    $('.select2').select2();

    this.productAttributeInfo.attributeType = 4;
    this.productAttributeInfo.isRequired = false;
    this.productAttributeInfo.baseUnitId = 1;
  }

  onSubmit(form: any, model: ProductAttributeInfoModel) {
    App.blockUI();
    this.formValid = form.valid;
    if (this.formValid) {
      this.setTab.emit('tab-3');
    } else {
      App.unblockUI();
    }
  }
}
