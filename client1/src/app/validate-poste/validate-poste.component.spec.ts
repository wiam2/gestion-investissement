import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidatePosteComponent } from './validate-poste.component';

describe('ValidatePosteComponent', () => {
  let component: ValidatePosteComponent;
  let fixture: ComponentFixture<ValidatePosteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ValidatePosteComponent]
    });
    fixture = TestBed.createComponent(ValidatePosteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
