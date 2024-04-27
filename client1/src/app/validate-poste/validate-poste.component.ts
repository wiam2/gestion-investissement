import {Component, ElementRef, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../Services/AuthService.service";
import {ActivatedRoute, Router} from "@angular/router";
import {DeletePosteModalService} from "../Services/DeletePosteModalService";

@Component({
  selector: 'app-validate-poste',
  templateUrl: './validate-poste.component.html',
  styleUrls: ['./validate-poste.component.css']
})
export class ValidatePosteComponent {
  @Input() size? = "md";
  @Output() closeEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter();

  constructor(private elementRef: ElementRef, private authservice: AuthService, private router: Router, private route: ActivatedRoute,
              private modalConfirmService: DeletePosteModalService) {
  }

  close() {
    this.elementRef.nativeElement.remove();
    this.closeEvent.emit();
  }

  submit() {
    this.elementRef.nativeElement.remove();
    this.submit
  }
}
