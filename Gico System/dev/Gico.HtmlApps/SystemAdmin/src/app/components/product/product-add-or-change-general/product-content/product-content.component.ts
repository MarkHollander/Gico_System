import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ConfigSetting } from '../../../../common/configSetting';
import { ProductContentModel } from '../../../../models/product-model/product-content-model';

declare var jquery: any;
declare var $: any;
declare var App: any;

@Component({
  selector: 'app-product-content',
  templateUrl: './product-content.component.html',
  styleUrls: ['./product-content.component.css']
})
export class ProductContentComponent implements OnInit {
  @Output() setTab = new EventEmitter<string>();

  productContent: ProductContentModel;
  formValid = true;

  constructor() { }

  ngOnInit() {
    this.productContent = new ProductContentModel();
    $('.select2').select2();
  }

  onSubmit(form: any, model: ProductContentModel) {
    App.blockUI();
    this.formValid = form.valid;
    if (this.formValid) {
      this.setTab.emit('tab-2');
    } else {
      App.unblockUI();
    }
  }
}
