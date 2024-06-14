import { NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MainPageComponent } from './main-page/main-page.component';
import { ProfileComponent } from './profile/profile.component';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { HttpClientModule } from '@angular/common/http';
import { DeleteProfileComponent } from './delete-profile/delete-profile.component';
import { EditPosteComponent } from './edit-poste/edit-poste.component';
import { DeleteposteComponent } from './deleteposte/deleteposte.component';
import { ValidatePosteComponent } from './validate-poste/validate-poste.component';
import { NotifiacationListComponent } from './notifiacation-list/notifiacation-list.component';
import { ConversationComponent } from './conversation/conversation.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,

    NavbarComponent,
     MainPageComponent,
     ProfileComponent,
     ChatboxComponent,
     EditProfileComponent,
     DeleteProfileComponent,
     EditPosteComponent,
     DeleteposteComponent,
     ValidatePosteComponent,
     NotifiacationListComponent,
     ConversationComponent


  ],
  imports: [

    BrowserModule,
    AppRoutingModule,
    RouterModule,
    FormsModule,
    HttpClientModule,


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
