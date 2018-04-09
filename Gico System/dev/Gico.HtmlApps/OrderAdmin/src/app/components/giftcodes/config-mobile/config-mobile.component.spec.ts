import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigMobileComponent } from './config-mobile.component';

describe('ConfigMobileComponent', () => {
  let component: ConfigMobileComponent;
  let fixture: ComponentFixture<ConfigMobileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigMobileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigMobileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
