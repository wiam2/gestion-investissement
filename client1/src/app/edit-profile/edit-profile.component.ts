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
    id : string = '';
    investisseur : Investisseur = new Investisseur();
    startup : Startup = new Startup();
 @Input() size?="md";
 @Output() closeEvent=new EventEmitter();
 @Output() submitEvent=new EventEmitter();
constructor(private elementRef:ElementRef,private authservice:AuthService ,private InvesService: InvestisseurService, private StartupService:StartupService,private router:Router ,  private route: ActivatedRoute,private modalService:modalService) {

}
    ngOnInit(): void {
        this.id ='100a7513-b1b4-4dbb-bacf-d9791ec415ae';
        if(this.authservice.currentUserRole()==="RInvestisseur"){
            console.log(this.authservice.currentUserRole())
            this.role = 'Invest';
            this.InvesService.GetProfileInv(this.id).subscribe(data =>{
                this.investisseur=data;

                console.log(data);
            }, error => console.log(error));

        } else {
            this.StartupService.GetProfileStartup(this.id).subscribe(data =>{
                this.startup=data;
console.log(this.startup);

            }, error => console.log(error));

        }


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
