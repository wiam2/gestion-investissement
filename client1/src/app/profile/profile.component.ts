import { Component, OnInit ,TemplateRef} from '@angular/core';
import { AuthService } from '../Services/AuthService.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Investisseur } from '../models/Investisseur.model';
import { Startup } from '../models/Startup.model';
import { InvestisseurService } from '../Services/InvestisseurService.service';
import { StartupService } from '../Services/StartupService.service';
import {modalService} from "../Services/modalService";
import {EditPosteModalService} from "../Services/EditPosteModalService";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  role: string|null = '';
  id : string = '';
  investisseur : Investisseur = new Investisseur();
  startup : Startup = new Startup ();
  constructor(private authservice:AuthService ,private InvesService: InvestisseurService, private StartupService: StartupService,private router:Router ,  private route: ActivatedRoute,private modalService:modalService,
              private modalPosteService:EditPosteModalService) { }

 ngOnInit(): void {
  this.id = this.route.snapshot.params['id'];
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

      console.log(data);
    }, error => console.log(error));

   }


 }



  openModal(modalTemplate:TemplateRef<any>) {
    this.modalService
      .open(modalTemplate)
      .subscribe((action)=>{
        console.log('modalAction',action);
      });
  }
    openModalPoste(modalPosteTemplate:TemplateRef<any>) {
        this.modalPosteService
            .open(modalPosteTemplate)
            .subscribe((action)=>{
                console.log('modalAction',action);
            });
    }



}
