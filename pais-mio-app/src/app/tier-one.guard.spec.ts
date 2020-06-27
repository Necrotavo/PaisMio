import { TestBed } from '@angular/core/testing';

import { TierOneGuard } from './tier-one.guard';

describe('TierOneGuard', () => {
  let guard: TierOneGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(TierOneGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
