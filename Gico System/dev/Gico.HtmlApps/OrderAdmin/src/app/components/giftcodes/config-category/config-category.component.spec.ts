import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigCategoryComponent } from './config-category.component';

describe('ConfigCategoryComponent', () => {
  let component: ConfigCategoryComponent;
  let fixture: ComponentFixture<ConfigCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
