import {Component, TemplateRef} from '@angular/core';
import {Router} from "@angular/router";
import { AuthService } from '../Services/AuthService.service';
import {NotifModalService} from "../Services/NotifModalService";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
   id : string |null ='' ;
  constructor(private authservice :AuthService,private router: Router,private NotifService:NotifModalService) { }
  logout(){
    this.authservice.logout();
    this.router.navigate(['./login']);
  }
  MyProfile() {
    const currentUser = this.authservice.currentUser();
    if (currentUser) {
      this.id = currentUser.id;
      console.log(this.id);

      this.router.navigate(['./profile', this.id]);
    }

  }
  mychat(){
    this.router.navigate(['./conversation']);

  }

  openModalNotif(modalPosteTemplate:TemplateRef<any>) {
    this.NotifService
      .open(modalPosteTemplate)
      .subscribe((action)=>{
        console.log('modalAction',action);
      });
  }

}
