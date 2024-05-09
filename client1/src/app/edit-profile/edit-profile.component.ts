import {Component, ElementRef, EventEmitter, OnInit, Output} from '@angular/core';
import { Input } from '@angular/core';
import {Investisseur} from "../models/Investisseur.model";
import {Startup} from "../models/Startup.model";
import {AuthService} from "../Services/AuthService.service";
import {InvestisseurService} from "../Services/InvestisseurService.service";
import {ActivatedRoute, Router} from "@angular/router";
import {StartupService} from "../Services/StartupService.service";
import {modalService} from "../Services/modalService";

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
    role: string|null = '';

    investisseur : Investisseur = new Investisseur();
    startup : Startup = new Startup();
  Success: boolean = false;
  @Input() id: string ="";
  @Input() dataedit: any;
  @Input() size?="md";
 @Output() closeEvent=new EventEmitter();
 @Output() submitEvent=new EventEmitter();
constructor(private elementRef:ElementRef,private authservice:AuthService ,private InvesService: InvestisseurService, private StartupService:StartupService,private router:Router ,  private route: ActivatedRoute,private modalService:modalService) {

}
    ngOnInit(): void {
        if(this.authservice.currentUserRole()==="RInvestisseur"){
            console.log(this.authservice.currentUserRole())
            this.role = 'Invest';
                this.investisseur=this.dataedit;
        } else {
                this.startup=this.dataedit;

        }


    }

close(){
   this.elementRef.nativeElement. remove();
   this.closeEvent.emit();
}
submit(){
  console.log(this.id);
  console.log(this.dataedit);
    if(this.authservice.currentUserRole()==="RInvestisseur"){
  this.investisseur.confirmPassword=this.investisseur.password;
  this.InvesService.UpdateInvest(this.id, this.investisseur).subscribe(data => {
      console.log(this.investisseur);
      this.Success = true;
      console.log(this.Success);
      console.log(data);
      location.reload();
    },
    error => console.log(error))}
    else{
      console.log(this.startup);
      console.log(this.id);
        this.startup.confirmPassword=this.startup.password;
        this.StartupService.UpdatStartUp(this.id,this.startup).subscribe(data=>{

  console.log(data);
  location.reload();
},error=>console.log(error))
    }

    this.elementRef.nativeElement. remove();
    this.submitEvent.emit();
}

}
