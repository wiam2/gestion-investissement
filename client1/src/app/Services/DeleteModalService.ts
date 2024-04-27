import {ComponentFactoryResolver, Injectable,Inject, Injector, TemplateRef} from '@angular/core';
import {EditProfileComponent} from "../edit-profile/edit-profile.component";
import {DOCUMENT} from "@angular/common";
import {Subject} from "rxjs";
import {DeleteProfileComponent} from "../delete-profile/delete-profile.component";


@Injectable({
  providedIn: 'root'
})
export class DeleteModalService{
  private modalNotifier?:Subject<string>
  constructor(private resolver:ComponentFactoryResolver,
              private injector:Injector,
              @Inject(DOCUMENT) private document:Document
  ) {
  }
  open(content :TemplateRef<any>){
    const modalComponentFactory=this.resolver.resolveComponentFactory(DeleteProfileComponent)
    const contentViewRef=content.createEmbeddedView(null)
    const modalComponent = modalComponentFactory.create(this.injector,[
      contentViewRef.rootNodes,
    ]);
    modalComponent.instance.closeEvent.subscribe(()=>
      this.closeModal());
    modalComponent.instance.submitEvent.subscribe(()=>
      this.submitModal());
    modalComponent.hostView.detectChanges() ;
    this.document.body.appendChild(modalComponent.location.nativeElement);
    this.modalNotifier=new Subject();
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
