import { Component, OnInit } from '@angular/core';
import { VideoCallService } from '../Services/VideoCallService';
import { AuthService } from '../Services/AuthService.service';
import { user } from '../models/user.model';
@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css']
})
export class ChatboxComponent implements OnInit {

  userChats: NodeListOf<Element>;
  chatMessages: NodeListOf<Element>;

  constructor(private videoCallService:VideoCallService, private authService :AuthService) {
    this.userChats = document.querySelectorAll('.user-chat');
    this.chatMessages = document.querySelectorAll('.content-chat-message-user');
  }

  ngOnInit(): void {
    // this.userChats = document.querySelectorAll('.user-chat');
    // this.chatMessages = document.querySelectorAll('.content-chat-message-user');

    // // Activer le premier élément user-chat initialement
    // this.userChats[0].classList.add('active');
    // this.chatMessages[0].classList.add('active');
  }

  selectUserChat(selectedUsername: string): void {
    this.chatMessages.forEach((chatMessage) => {
      const messageUsername = chatMessage.getAttribute('data-username');
      if (messageUsername === selectedUsername) {
        chatMessage.classList.add('active');
      } else {
        chatMessage.classList.remove('active');
      }
    });

    this.userChats.forEach((chat) => {
      chat.classList.remove('active');
    });

    const selectedChat = document.querySelector(`.user-chat[data-username="${selectedUsername}"]`);
    if (selectedChat) {
      selectedChat.classList.add('active');
    }
  }
  startVideoCall(): void {
// Récupérer l'utilisateur actuel
const currentUser : user | null = this.authService.currentUser();

// Vérifier si currentUser est défini
if (currentUser) {
  // Appeler la méthode handleNewMeeting avec l'utilisateur actuel
  this.videoCallService.handleNewMeeting(currentUser);
} else {
  // Afficher une erreur si l'utilisateur actuel n'est pas défini
  console.error('Utilisateur actuel non défini.');
}


}
}
