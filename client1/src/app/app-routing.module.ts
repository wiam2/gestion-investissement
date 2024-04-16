import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {SignupComponent} from "./signup/signup.component";
import {LoginComponent} from "./login/login.component";
import {HomeComponent} from "./home/home.component";
import {NavbarComponent} from "./navbar/navbar.component";
import {MainPageComponent} from "./main-page/main-page.component";
import {ProfileComponent} from "./profile/profile.component";
import {ChatboxComponent} from "./chatbox/chatbox.component";
import {EditProfileComponent } from "./edit-profile/edit-profile.component"
const routes: Routes = [
  {
    path:'',
    component:HomeComponent
  },
  {
    path:'page',
    component:MainPageComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'signup',
    component:SignupComponent
  },
  {
    path:'nav',
    component:NavbarComponent
  },
  {
    path:'profile',
    component:ProfileComponent
  },
  {
    path:'chat',
    component:ChatboxComponent
  },
  {
    path:'editProfile',
    component:EditProfileComponent
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
