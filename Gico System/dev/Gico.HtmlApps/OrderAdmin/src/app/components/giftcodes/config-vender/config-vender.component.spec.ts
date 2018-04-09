import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigVenderComponent } from './config-vender.component';

describe('ConfigVenderComponent', () => {
  let component: ConfigVenderComponent;
  let fixture: ComponentFixture<ConfigVenderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigVenderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigVenderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
