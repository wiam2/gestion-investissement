import {Component, ElementRef, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AuthService} from "../Services/AuthService.service";
import {ActivatedRoute, Router} from "@angular/router";
import {DeletePosteModalService} from "../Services/DeletePosteModalService";
import { PosteService } from '../Services/PosteService.service';

@Component({
  selector: 'app-validate-poste',
  templateUrl: './validate-poste.component.html',
  styleUrls: ['./validate-poste.component.css']
})
export class ValidatePosteComponent implements OnInit{
  @Input() size? = "md";
  @Input() idPoste: any;
  @Output() closeEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter();
role:string="";
  constructor(private elementRef: ElementRef, private authservice: AuthService, private router: Router, private route: ActivatedRoute,
              private modalConfirmService: DeletePosteModalService,private posteService:PosteService) {
  }
ngOnInit(): void {
  if (this.authservice.currentUserRole() === "RInvestisseur") {

    this.role = 'Invest';
    console.log(this.role);
  }
  else {
    this.role = 'Star';
  }
}
  close() {
    this.elementRef.nativeElement.remove();
    this.closeEvent.emit();
  }

  submit() {
    if (this.role == 'Invest') {
      this.posteService.ValiderInv(this.idPoste).subscribe(data => {
          console.log(data);
        });
      this.elementRef.nativeElement.remove();
      this.submitEvent.emit();
    }
    else {
      this.posteService.ValiderStar(this.idPoste).subscribe(data => {
        console.log(data);
      });
      this.elementRef.nativeElement.remove();
      this.submitEvent.emit();
    }
    this.elementRef.nativeElement.remove();
    this.submit
  }
}
