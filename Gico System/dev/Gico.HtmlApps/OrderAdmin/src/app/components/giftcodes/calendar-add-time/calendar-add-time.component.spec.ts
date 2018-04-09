import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarAddTimeComponent } from './calendar-add-time.component';

describe('CalendarAddTimeComponent', () => {
  let component: CalendarAddTimeComponent;
  let fixture: ComponentFixture<CalendarAddTimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarAddTimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarAddTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
