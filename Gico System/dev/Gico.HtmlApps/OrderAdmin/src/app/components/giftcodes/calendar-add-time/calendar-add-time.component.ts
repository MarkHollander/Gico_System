import { Component, OnInit } from '@angular/core';
import { GiftCodeCalendarMinuteModel,time } from '../../../models/giftcodes/gift-code-calendar-time-model'
import { Guid } from '../../../common/guid'
import { forEach } from '@angular/router/src/utils/collection';
import { ConfigSetting } from '../../../common/configSetting';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-calendar-add-time',
  templateUrl: './calendar-add-time.component.html',
  styleUrls: ['./calendar-add-time.component.css']
})
export class CalendarAddTimeComponent implements OnInit {
  date: Date;
  times: GiftCodeCalendarMinuteModel[];
  isApplyAll: boolean;
  constructor() { }

  ngOnInit() {
    this.times = [];
    this.isApplyAll = false;
    this.onAddTime();
  }
  async onRegisterTime(): Promise<void> {

  }
  async onAddTime(): Promise<void> {  
    let timeModel = new GiftCodeCalendarMinuteModel();
    let i=this.times.length;
    timeModel.begin = i > 0 ? this.times[this.times.length - 1].end : new time(0, 0);
    timeModel.end = this.convertMinute(timeModel.beginMinute + 5);
    timeModel.key = Guid.newGuid();
    this.times.push(timeModel);
  }
  async onRemoveTime(key: string): Promise<void> {
    for (let i = 0; i < this.times.length; i++) {
      if (this.times[i].key == key) {
        this.times.splice(i, 1);
        return;
      }
    }
  }
  async onTimeFocus(event: any,key: string): Promise<void> {
   // $(".bootstrap-timepicker-hour").val($(event.target.value.toString().split(':')[0]));
   
   if ($(event.target).hasClass("time-registed")) {
      return;
    }
    $(event.target).addClass("time-registed");
    $(event.target).timepicker({
      autoclose: true,
      minuteStep: 5,
      showSeconds: false,
      showMeridian: false,
    });
    
  } 
  async validTime(): Promise<void> {
    let invalid = false;
    for (let i = 0; i < this.times.length; i++) {
      if (i > 0) {
        if (this.times[i - 1].endMinute > this.times[i].beginMinute) {
          this.times[i].begin = this.times[i - 1].end;
          invalid = true;
        }
      }
      if (this.times[i].endMinute <= this.times[i].beginMinute) {
        this.times[i].end = this.convertMinute(this.times[i].beginMinute + 5);
        invalid = true;
      }
    }
    if (invalid) {
      ConfigSetting.ShowError("The times you entered is not valid.It is automatically set");
    }
  }   
  convertMinute(minute: number): time {
    let _time = new time(Math.floor(minute / 60), minute % 60);
    return _time;
  }  
}
