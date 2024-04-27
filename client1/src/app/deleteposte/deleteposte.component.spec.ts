import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteposteComponent } from './deleteposte.component';

describe('DeleteposteComponent', () => {
  let component: DeleteposteComponent;
  let fixture: ComponentFixture<DeleteposteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteposteComponent]
    });
    fixture = TestBed.createComponent(DeleteposteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
