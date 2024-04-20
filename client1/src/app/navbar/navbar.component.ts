import { Component } from '@angular/core';
import {Router} from "@angular/router";
import { AuthService } from '../Services/AuthService.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
   id : string |null ='' ;
  constructor(private authservice :AuthService,private router: Router) { }
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
    this.router.navigate(['./chat']);

  }

}
