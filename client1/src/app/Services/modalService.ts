import {ComponentFactoryResolver, Injectable, Inject, Injector, TemplateRef, EventEmitter, OnInit} from '@angular/core';
import {EditProfileComponent} from "../edit-profile/edit-profile.component";
import {DOCUMENT} from "@angular/common";
import {Subject} from "rxjs";
import {InvestisseurService} from "./InvestisseurService.service";
import {StartupService} from "./StartupService.service";
import {Investisseur} from "../models/Investisseur.model";
import {Startup} from "../models/Startup.model";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthService} from "./AuthService.service";



@Injectable({
  providedIn: 'root'
})
export class modalService  {

    role: string|null = '';
    id : string = '';
    investisseur : Investisseur = new Investisseur();
    startup : Startup = new Startup();

private modalNotifier?:Subject<string>
  constructor(private resolver:ComponentFactoryResolver,
              private injector:Injector,
              @Inject(DOCUMENT) private document:Document,
              private InvesService:InvestisseurService,
               private StartupService:StartupService,
              private router:Router ,
              private route: ActivatedRoute,private authservice:AuthService
  ) {
  }

  open(content: TemplateRef<any>, id: string,Data:any) {
    const modalComponentFactory = this.resolver.resolveComponentFactory(EditProfileComponent);
    const contentViewRef = content.createEmbeddedView(null);
    const modalComponent = modalComponentFactory.create(this.injector, [
      contentViewRef.rootNodes,
    ]);
    modalComponent.instance.dataedit = Data;
    console.log(Data);
    modalComponent.instance.id = id; // Transmettez l'id au composant de popup
    modalComponent.instance.closeEvent.subscribe(() => this.closeModal());
    modalComponent.instance.submitEvent.subscribe(() => this.submitModal());
    modalComponent.hostView.detectChanges();
    this.document.body.appendChild(modalComponent.location.nativeElement);
    this.modalNotifier = new Subject();
    return this.modalNotifier?.asObservable();
  }

   closeModal(){
   this.modalNotifier?.complete();
   }
   submitModal(){
     this.modalNotifier?.next('comfirm');
     this.closeModal();
   }

}


