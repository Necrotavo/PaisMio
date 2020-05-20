import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexPmAppComponent } from './index-pm-app.component';

describe('IndexPmAppComponent', () => {
  let component: IndexPmAppComponent;
  let fixture: ComponentFixture<IndexPmAppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IndexPmAppComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IndexPmAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
