import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenedchartComponent } from './openedchart.component';

describe('OpenedchartComponent', () => {
  let component: OpenedchartComponent;
  let fixture: ComponentFixture<OpenedchartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpenedchartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenedchartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
