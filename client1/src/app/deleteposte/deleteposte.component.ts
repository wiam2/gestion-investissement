import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from "../Services/AuthService.service";
import { ActivatedRoute, Router } from "@angular/router";

import { DeletePosteModalService } from "../Services/DeletePosteModalService";
import { PosteService } from '../Services/PosteService.service';

@Component({
  selector: 'app-deleteposte',
  templateUrl: './deleteposte.component.html',
  styleUrls: ['./deleteposte.component.css']
})
export class DeleteposteComponent implements OnInit {
  @Input() size? = "md";
  @Input() idPoste: any;
  @Output() closeEvent = new EventEmitter();
  @Output() submitEvent = new EventEmitter();
  role: string = "";
  constructor(private elementRef: ElementRef, private authservice: AuthService, private router: Router, private route: ActivatedRoute,
    private modalDeleteService: DeletePosteModalService, private posteService: PosteService) {
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

  confirmer() {

    console.log(this.idPoste);

  }
  submit() {
    // console.log(this.idPoste);
    // this.posteService.deletePosteInv(this.idPoste).subscribe(data => {
    //   console.log(data);
    // });
    // this.elementRef.nativeElement.remove();
    // this.submitEvent.emit();


    if (this.role == 'Invest') {
      this.posteService.deletePosteInv(this.idPoste).subscribe(data => {
          console.log(data);
        });
      this.elementRef.nativeElement.remove();
      this.submitEvent.emit();
    }
    else {
      this.posteService.deletePosteStar(this.idPoste).subscribe(data => {
        console.log(data);
      });
      this.elementRef.nativeElement.remove();
      this.submitEvent.emit();
    }

  }
}
