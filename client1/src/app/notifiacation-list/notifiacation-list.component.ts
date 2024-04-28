import {Component, ElementRef, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../Services/AuthService.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EditPosteModalService} from "../Services/EditPosteModalService";
import {NotifModalService} from "../Services/NotifModalService";

@Component({
  selector: 'app-notifiacation-list',
  templateUrl: './notifiacation-list.component.html',
  styleUrls: ['./notifiacation-list.component.css']
})
export class NotifiacationListComponent {
  @Input() size?="md";
  @Output() closeEvent=new EventEmitter();
  @Output() submitEvent=new EventEmitter();
  constructor(private elementRef:ElementRef,private authservice:AuthService ,private router:Router ,  private route: ActivatedRoute,private modalNotifService:NotifModalService) {
  }
  close(){
    this.elementRef.nativeElement. remove();
    this.closeEvent.emit();
  }
  submit(){
    this.elementRef.nativeElement. remove();
    this.submitEvent.emit();
  }
}
