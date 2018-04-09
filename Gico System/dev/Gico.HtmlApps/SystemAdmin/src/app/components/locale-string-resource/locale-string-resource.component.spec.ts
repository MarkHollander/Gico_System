import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LocaleStringResourceComponent } from './locale-string-resource.component';

describe('LocaleStringResourceComponent', () => {
  let component: LocaleStringResourceComponent;
  let fixture: ComponentFixture<LocaleStringResourceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LocaleStringResourceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LocaleStringResourceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
