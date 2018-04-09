import { Component, OnInit } from '@angular/core';
import { WarehouseAddOrChangeModel } from '../../../models/warehouse/warehouse-add-or-change-model';
import { KeyValueModel } from '../../../models/result-model';
import { WarehouseService } from '../../../services/warehouse.service';
import { ConfigSetting } from '../../../common/configSetting';

declare var App: any;
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-warehouse-add-or-change',
  templateUrl: './warehouse-add-or-change.component.html',
  styleUrls: ['./warehouse-add-or-change.component.css']
})
export class WarehouseAddOrChangeComponent implements OnInit {

  warehouse: WarehouseAddOrChangeModel;
  types: KeyValueModel[];
  statuses: KeyValueModel[];
  title: string;
  constructor(
    private warehouseService: WarehouseService
  ) { }

  ngOnInit() {
    this.warehouse = new WarehouseAddOrChangeModel();
    this.warehouse.id = '';

  }


  async onShowWarehouseAddress(id: string): Promise<void> {
    try {
    
    
      // this.warehouseAddOrChange.warehouse.id = id;
      // console.log("hahah");
      
      // console.log(this.warehouseAddOrChange.warehouse);
      // this.warehouseAddOrChange.onGet();
      $('#warehouse-address-add-or-change').modal('show');
      //$("#warehouse-add-or-change").modal('toggle');

    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }

  async onGet(): Promise<void> {
    App.blockUI();
    try {
      if (this.warehouse.id.length == 0)
        this.title = "Add new warehouse";
      else
        this.title = "Edit warehouse";
      var response = await this.warehouseService.get(this.warehouse.id);
      this.warehouse = response.warehouse;
      console.log("popo");
      console.log(this.warehouse);
      this.types = response.types;
      this.statuses = response.statuses;

    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
    App.unblockUI();
  }
  trackByIndex(index: number, obj: any): any {
    return index;
  }

}
