import { TestBed } from '@angular/core/testing';

import { TierThreeGuard } from './tier-three.guard';

describe('TierThreeGuard', () => {
  let guard: TierThreeGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(TierThreeGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
