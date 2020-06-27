import { TestBed } from '@angular/core/testing';

import { TierTwoGuard } from './tier-two.guard';

describe('TierTwoGuard', () => {
  let guard: TierTwoGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(TierTwoGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
