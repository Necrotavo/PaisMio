import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportComparativeComponent } from './report-comparative.component';

describe('ReportComparativeComponent', () => {
  let component: ReportComparativeComponent;
  let fixture: ComponentFixture<ReportComparativeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportComparativeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportComparativeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
