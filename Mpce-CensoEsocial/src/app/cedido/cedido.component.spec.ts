import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CedidoComponent } from './cedido.component';

describe('CedidoComponent', () => {
  let component: CedidoComponent;
  let fixture: ComponentFixture<CedidoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CedidoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CedidoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
