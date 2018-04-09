import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigDeviceTypeComponent } from './config-device-type.component';

describe('ConfigDeviceTypeComponent', () => {
  let component: ConfigDeviceTypeComponent;
  let fixture: ComponentFixture<ConfigDeviceTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigDeviceTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigDeviceTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
