import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ProductVariantModel } from '../../../../models/product-model/product-variant-model';
import { KeyValueModel } from '../../../../models/result-model';

declare var jquery: any;
declare var $: any;
declare var App: any;

@Component({
  selector: 'app-product-variant',
  templateUrl: './product-variant.component.html',
  styleUrls: ['./product-variant.component.css']
})
export class ProductVariantComponent implements OnInit {
  @Output() setTab = new EventEmitter<string>();

  productVariant: ProductVariantModel;
  formValid = true;
  cateNote;

  constructor() { }

  ngOnInit() {
    this.productVariant = new ProductVariantModel();
    this.productVariant.attributeVariants = [];
    this.cateNote = '';
    $('.select2').select2();
  }

  onSubmit(form: any, model: ProductVariantModel) {
    App.blockUI();
    this.formValid = form.valid;
    if (this.formValid) {
      this.setTab.emit('tab-5');
    } else {
      App.unblockUI();
    }
  }
}
