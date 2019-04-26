import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DependenteComponent } from './dependente.component';

describe('DependenteComponent', () => {
  let component: DependenteComponent;
  let fixture: ComponentFixture<DependenteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DependenteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DependenteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
