import { TestBed, inject } from '@angular/core/testing';

import { LocaleStringResourceService } from './locale-string-resource.service';

describe('LocaleStringResourceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LocaleStringResourceService]
    });
  });

  it('should be created', inject([LocaleStringResourceService], (service: LocaleStringResourceService) => {
    expect(service).toBeTruthy();
  }));
});
