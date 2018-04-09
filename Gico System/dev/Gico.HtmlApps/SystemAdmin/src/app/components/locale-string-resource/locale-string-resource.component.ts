import { Component, OnInit, ViewChild, OnChanges } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { ConfigSetting } from '../../common/configSetting';
import { LocaleStringResourceService } from '../../services/locale-string-resource.service';


import { KeyValueModel } from '../../models/result-model';
import { Dictionary } from '../../models/dictionary';
import { forEach } from '@angular/router/src/utils/collection';
import { LocaleStringResourceModel } from '../../models/locale-string-resource-model';
import { LocaleStringResourceSearchRequestModel } from '../../models/locale-string-resource-search-request-model';

declare var jquery: any;
declare var $: any;
declare var App: any;

@Component({
  selector: 'app-locale-string-resource',
  templateUrl: './locale-string-resource.component.html',
  styleUrls: ['./locale-string-resource.component.css']
})
export class LocaleStringResourceComponent implements OnInit {
  private languageId: string;
  searchParams: LocaleStringResourceSearchRequestModel;
  locales: LocaleStringResourceModel[];
  languages: KeyValueModel[];
  localeAdnew: LocaleStringResourceModel;
  localeEdit: LocaleStringResourceModel;
  showAddNew = false;
  rowEdits: Dictionary<boolean>;
  pageIndex: number = 0;
  pageSize = 30;
  totalRow = 2;
  constructor(
    private localeService: LocaleStringResourceService,
  ) { }

  @ViewChild('localeForm') form1: any;

  ngOnInit() {
    this.searchParams = new LocaleStringResourceSearchRequestModel();
    this.locales = [];
    this.languages = [];
    this.localeAdnew = new LocaleStringResourceModel();
    this.localeEdit = new LocaleStringResourceModel();
    this.rowEdits = new Dictionary<boolean>();
    this.onSearch();

  }

  async onSearch(): Promise<void> {
    try {
      let response = await this.localeService.search(this.searchParams);
      this.locales = response.locales as LocaleStringResourceModel[];
      this.languages = response.languages;
      this.totalRow = response.totalRow;
      this.rowEdits = new Dictionary<boolean>();
      for (var i = 0; i < this.locales.length; i++) {
        var locale = this.locales[i];
        this.rowEdits.Add(locale.id, false);
      }

    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
  async onAddNew(): Promise<void> {
    try {
      this.showAddNew = !this.showAddNew;
      this.localeAdnew = new LocaleStringResourceModel();
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
  async onAddNewCancel(): Promise<void> {
    try {
      this.showAddNew = false;;
    }
    catch (ex) {
      ConfigSetting.ShowErrorException(ex);
    }
  }
  async onChange(id: string): Promise<void> {
    this.onAddNewCancel();
    for(let i=0;i<this.locales.length;i++){
      if(this.rowEdits.Item(this.locales[i].id)) {
        this.onChangeCancel(this.locales[i].id);
      }
    }
    let locale = this.locales.find(x => x.id == id);
    this.localeEdit = JSON.parse(JSON.stringify(locale));
    var state = this.rowEdits.Item(id);
    this.rowEdits.Change(id, !state);
  }
  async onChangeCancel(id: string): Promise<void> {
    this.rowEdits.Change(id, false);
    let index = this.locales.findIndex(x => x.id == id);
    this.locales[index] = this.localeEdit;
  }
  async onSave(id: string): Promise<void> {
    App.blockUI();
    if (this.form1.valid) {
      try {
        let locale: LocaleStringResourceModel = null;
        if (id == "") {
          locale = this.localeAdnew;
        }
        else {
          for (let i = 0; i <= this.locales.length; i++) {
            if (this.locales[i].id == id) {
              locale = this.locales[i];
              break;
            }
          }
        }
        if (locale == null) {
          ConfigSetting.ShowSuccess("Locale not null.");
        }
        else {
          let response = await this.localeService.save(locale);
          if (response.status) {
            ConfigSetting.ShowSuccess("Save sucess.");
            await this.onSearch();
            if (id == "") {
              await this.onAddNewCancel();
            }
            else {
              await this.rowEdits.Change(id, false);
            }
          }
          else {
            ConfigSetting.ShowErrores(response.messages);
          }
        }
      }
      catch (ex) {
        ConfigSetting.ShowErrorException(ex);
      }
    }

    App.unblockUI();
  }
}

