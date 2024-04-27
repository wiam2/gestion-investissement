import {Component, ElementRef, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../Services/AuthService.service";
import {ActivatedRoute, Router} from "@angular/router";
import {DeleteModalService} from "../Services/DeleteModalService";

@Component({
  selector: 'app-delete-profile',
  templateUrl: './delete-profile.component.html',
  styleUrls: ['./delete-profile.component.css']
})
export class DeleteProfileComponent {
  @Input() size?="md";
  @Output() closeEvent=new EventEmitter();
  @Output() submitEvent=new EventEmitter();
  constructor(private elementRef:ElementRef,private authservice:AuthService ,private router:Router ,  private route: ActivatedRoute,
              private modalDeleteService:DeleteModalService) {
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
