import { Component } from '@angular/core';
import { VideoCallService } from '../Services/VideoCallService';
import { AuthService } from '../Services/AuthService.service';
import { user } from '../models/user.model';
import { ChatService } from '../Services/chat.service';
import { AfterViewChecked,  ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ConversationService} from '../Services/conversation.service';
import { R3SelectorScopeMode } from '@angular/compiler';
import { ChatboxComponent } from '../chatbox/chatbox.component';
@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {
  router = inject(Router);
  chatService = inject(ChatService);
  conversations: any[] = [];
  useremail:string ='';
  mycontact :string ='';
  childData :string='';
  constructor(private videoCallService:VideoCallService, private authService :AuthService , private conversationService :ConversationService ) {
    
  }
  ngOnInit(): void {
    const currentUser = this.authService.currentUser();
    this.useremail = currentUser ? currentUser.email : ''; // Utilisation d'une valeur par défaut ('' pour une chaîne vide)
    
    if (this.useremail) {
        this.getConversationsByUsername(this.useremail);
    }
}
  getConversationsByUsername(username: string): void {
    this.conversationService.getConversations(username)
      .subscribe(
        (data: any) => {
          // Convertir les données en tableau d'objets
          this.conversations = Object.entries(data).map(([key, value]) => ({ id: key, name: value }));
          console.log(this.conversations);
        },
        (error) => {
          console.error(error);
          // Gérer l'erreur ici
        }
      );
  }

  joinRoom(user: string, room: string , name : string) {
    console.log(user, room); 
    sessionStorage.clear(); 

     this.chatService.clearMessages();
     this.chatService.start().then(()=>this.chatService.joinRoom(user, room)
     .then(() => {
       sessionStorage.setItem("me", user);
       sessionStorage.setItem("contact",room);
      
      
       this.router.navigate(['conversation/chat',name]);
     }).catch((err) => {
       console.log(err);
     })) ; // Reconnexion à SignalR après avoir rejoint la salle
    
  }
  gotopage(){
   
  
      this.chatService.leaveChat();
     
  
    
    this.router.navigate(['page']);
  }
  
}
