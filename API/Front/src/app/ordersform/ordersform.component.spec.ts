import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OrdersFormComponent } from './ordersform.component';

describe('OrdersFormComponent', () => {
  let component: OrdersFormComponent;
  let fixture: ComponentFixture<OrdersFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ OrdersFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
