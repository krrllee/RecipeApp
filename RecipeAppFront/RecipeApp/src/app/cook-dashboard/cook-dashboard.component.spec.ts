import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CookDashboardComponent } from './cook-dashboard.component';

describe('CookDashboardComponent', () => {
  let component: CookDashboardComponent;
  let fixture: ComponentFixture<CookDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CookDashboardComponent]
    });
    fixture = TestBed.createComponent(CookDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
