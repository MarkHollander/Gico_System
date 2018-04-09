import { Component } from '@angular/core';
import { ConfigSetting } from '../../common/configSetting';
import { Dictionary } from '../../models/dictionary';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.css']
})
export class BaseComponent {
  permissions: Dictionary<boolean>;
  actionIds: string[];
  constructor(actionIds: string[]) {
    this.actionIds = actionIds;
    this.onInit();
  }
  async onInit(): Promise<void> {
    try {
      if(this.actionIds!=null && this.actionIds.length>0)
      {
        this.permissions = ConfigSetting.CheckPermission(this.actionIds);  
      }      
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
}
