import { Component, OnInit, ViewChild } from '@angular/core';
import { WarehouseService } from '../../../services/warehouse.service';
import { WarehouseSearchRequest } from '../../../models/warehouse/warehouse-search-request';
import { ConfigSetting } from '../../../common/configSetting';
import { KeyValueModel } from '../../../models/result-model';
import { WarehouseModel } from '../../../models/warehouse/warehouse-model';
import { WarehouseAddOrChangeComponent } from '../warehouse-add-or-change/warehouse-add-or-change.component';
import { WarehouseAddOrChangeModel } from '../../../models/warehouse/warehouse-add-or-change-model';


declare var App: any;
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css']
})
export class WarehouseComponent implements OnInit {
  @ViewChild(WarehouseAddOrChangeComponent) warehouseAddOrChange: WarehouseAddOrChangeComponent;


  searchParams: WarehouseSearchRequest;
  types: KeyValueModel[];
  statuses: KeyValueModel[];
  warehouses:WarehouseModel[];
  
  constructor(
     private warehouseService: WarehouseService
    
    ) { }

  ngOnInit() {
    this.searchParams = new WarehouseSearchRequest();
    this.getWarehouse();
  }

  async getWarehouse(): Promise<void> {
    // if (this.form.valid) {
      try {
        let response = await this.warehouseService.search(this.searchParams);
        console.log(response);
        this.types = response.types;
        this.statuses = response.statuses;
        this.warehouses = response.warehouses;
      
       // this.totalRow = response.totalRow;
     
      }
      catch (ex) {
        ConfigSetting.ShowErrorException(ex);
      }
    // }
  }

  async onShowAddOrChangeForm(id: string): Promise<void> {
    try {
    
    
      this.warehouseAddOrChange.warehouse.id = id;
      console.log("hahah");
      
      console.log(this.warehouseAddOrChange.warehouse);
      this.warehouseAddOrChange.onGet();
      $('#warehouse-add-or-change').modal('show');
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }

  

}
