import { TestBed, inject } from '@angular/core/testing';

import { GiftcodeService } from './giftcode.service';

describe('GiftcodeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GiftcodeService]
    });
  });

  it('should be created', inject([GiftcodeService], (service: GiftcodeService) => {
    expect(service).toBeTruthy();
  }));
});
