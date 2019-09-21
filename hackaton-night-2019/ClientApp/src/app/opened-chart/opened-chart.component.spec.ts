import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenedChartComponent } from './opened-chart.component';

describe('OpenedChartComponent', () => {
  let component: OpenedChartComponent;
  let fixture: ComponentFixture<OpenedChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpenedChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenedChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
