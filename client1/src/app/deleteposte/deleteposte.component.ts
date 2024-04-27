import {Component, ElementRef, EventEmitter, Input, Output} from '@angular/core';
import {AuthService} from "../Services/AuthService.service";
import {ActivatedRoute, Router} from "@angular/router";

import {DeletePosteModalService} from "../Services/DeletePosteModalService";

@Component({
  selector: 'app-deleteposte',
  templateUrl: './deleteposte.component.html',
  styleUrls: ['./deleteposte.component.css']
})
export class DeleteposteComponent {
  @Input() size? = "md";
  @Output() closeEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter();

  constructor(private elementRef: ElementRef, private authservice: AuthService, private router: Router, private route: ActivatedRoute,
              private modalDeleteService: DeletePosteModalService) {
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
