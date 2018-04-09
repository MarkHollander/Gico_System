import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigPaymentTypeComponent } from './config-payment-type.component';

describe('ConfigPaymentTypeComponent', () => {
  let component: ConfigPaymentTypeComponent;
  let fixture: ComponentFixture<ConfigPaymentTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigPaymentTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigPaymentTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
