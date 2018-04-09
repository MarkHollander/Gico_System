import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigProvinceComponent } from './config-province.component';

describe('ConfigProvinceComponent', () => {
  let component: ConfigProvinceComponent;
  let fixture: ComponentFixture<ConfigProvinceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigProvinceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigProvinceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
