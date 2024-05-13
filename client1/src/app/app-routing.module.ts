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
import { PageGuard } from './guards/pageguard.guard';
import {DeleteProfileComponent} from "./delete-profile/delete-profile.component";
import {EditPosteComponent} from "./edit-poste/edit-poste.component";
import {NotifiacationListComponent} from "./notifiacation-list/notifiacation-list.component";
import { ConversationComponent } from './conversation/conversation.component';
const routes: Routes = [
  {
    path:'',
    component:HomeComponent
  },
  {
    path:'page',
    component:MainPageComponent ,canActivate: [PageGuard]
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
    path:'profile/:id',
    component:ProfileComponent
  },
  
   
      {path:'conversation', component:ConversationComponent ,children:[
        {path:'chat/:email', component:ChatboxComponent}]}
      
      
,

  {
    path:'deleteProfile',
    component:DeleteProfileComponent
  },
  {
    path:'editPoste',
    component:EditPosteComponent
  },

  {
    path:'notif',
    component:NotifiacationListComponent
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }