import { TestBed } from '@angular/core/testing';

import { GrowerService } from './grower.service';

describe('GrowerService', () => {
  let service: GrowerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GrowerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
