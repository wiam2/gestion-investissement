import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotifiacationListComponent } from './notifiacation-list.component';

describe('NotifiacationListComponent', () => {
  let component: NotifiacationListComponent;
  let fixture: ComponentFixture<NotifiacationListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NotifiacationListComponent]
    });
    fixture = TestBed.createComponent(NotifiacationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
