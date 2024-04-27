import {Component, ElementRef, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../Services/AuthService.service";
import {InvestisseurService} from "../Services/InvestisseurService.service";
import {ActivatedRoute, Router} from "@angular/router";
import{EditPosteModalService} from "../Services/EditPosteModalService";

@Component({
  selector: 'app-edit-poste',
  templateUrl: './edit-poste.component.html',
  styleUrls: ['./edit-poste.component.css']
})
export class EditPosteComponent {
  @Input() size?="md";
  @Output() closeEvent=new EventEmitter();
  @Output() submitEvent=new EventEmitter();
  constructor(private elementRef:ElementRef,private authservice:AuthService ,private router:Router ,  private route: ActivatedRoute,private modalPosteService:EditPosteModalService) {
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
