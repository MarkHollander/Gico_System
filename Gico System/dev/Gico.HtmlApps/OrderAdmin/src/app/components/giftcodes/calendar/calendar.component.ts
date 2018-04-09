import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarAddTimeComponent } from '../calendar-add-time/calendar-add-time.component'
declare var App: any;
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  @ViewChild(CalendarAddTimeComponent) calendarAddTime: CalendarAddTimeComponent;
  beginDate: Date;
  endDate: Date;
  currentDate: Date;
  constructor() { }

  ngOnInit() {
    this.currentDate = new Date();
  }
  async registerCalendar(): Promise<void> {
    $('#calendar').fullCalendar( 'destroy' );
    let beginDate = this.beginDate;
    let endDate = this.endDate;
    let calendarAddTime = this.calendarAddTime;
    $('#calendar').fullCalendar({
      dayClick: function (date, jsEvent, view) {
        if (date < beginDate || date > endDate) {
          return;
        }
        $('#giftcode-calendar-addtime').modal('show');
        calendarAddTime.date=date;
        calendarAddTime.onRegisterTime();
      },
      viewRender: function (view, element) {
        if (beginDate >= view.start && beginDate <= view.end) {
          $(".fc-prev-button").prop('disabled', true);
          $(".fc-prev-button").addClass('fc-state-disabled');
        }
        else {
          $(".fc-prev-button").removeClass('fc-state-disabled');
          $(".fc-prev-button").prop('disabled', false);
        }
        // Future
        if (endDate >= view.start && endDate <= view.end) {
          $(".fc-next-button").prop('disabled', true);
          $(".fc-next-button").addClass('fc-state-disabled');
        } else {
          $(".fc-next-button").removeClass('fc-state-disabled');
          $(".fc-next-button").prop('disabled', false);
        }
      },
      dayRender: function (date, cell) {
        if (date._d < beginDate || date._d > endDate) {
          //$(cell).addClass('disabled');
        } else {
          //$(cell).addClass('active');
          $(cell).css('background-color', '#C8D046');
        }
      }
    });

    let uiConfig = {
      calendar: {
        height: 450,
        editable: true,
        header: {
          left: 'month',
          center: 'title',
          right: 'prev,next'
        },
        //defaultDate: currentDate,
        defaultView: 'month',
        dayClick: function (date, allDay, jsEvent, view) {
          debugger;
          // $scope.curentDate = date._d;
          // if (date._d < $scope.Model.BeginDateTime || date._d > $scope.Model.BeginEndTime) {
          //     return false;
          // }

          // $scope.TimeMssage = "";
          // $scope.Model.Times = [{ Begin: "", End: "", CurrentDate: date._d }];
          // $("#addTime").modal("show");
          // setTimeout(function () {
          //     $scope.maskInput();
          // }, 100);
        },
        viewRender: function (currentView) {
          // var minDate = beginDateTime;
          // var maxDate = beginEndTime;
          // // Past
          // if (minDate >= currentView.start && minDate <= currentView.end) {
          //   $(".fc-prev-button").prop('disabled', true);
          //   $(".fc-prev-button").addClass('fc-state-disabled');
          // }
          // else {
          //   $(".fc-prev-button").removeClass('fc-state-disabled');
          //   $(".fc-prev-button").prop('disabled', false);
          // }
          // // Future
          // if (maxDate >= currentView.start && maxDate <= currentView.end) {
          //   $(".fc-next-button").prop('disabled', true);
          //   $(".fc-next-button").addClass('fc-state-disabled');
          // } else {
          //   $(".fc-next-button").removeClass('fc-state-disabled');
          //   $(".fc-next-button").prop('disabled', false);
          // }
        },
        dayRender: function (date, cell) {
          // var now = beginDateTime;
          // var end = beginEndTime;
          // if (date._d < now || date._d > end) {
          //   $(cell).addClass('disabled');
          // } else {
          //   $(cell).addClass('active');
          // }
        },
        eventRender: function (event, element) {
          // angular.element(element).append($compile('<a class="event-calendar"  ng-click="deleteEvent(' + event._id + ')"> <i class="fa fa-remove"></i></a>')($scope));
        }
      }
    };
  }
}
