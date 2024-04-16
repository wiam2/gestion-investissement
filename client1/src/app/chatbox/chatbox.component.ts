import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css']
})
export class ChatboxComponent implements OnInit {

  userChats: NodeListOf<Element>;
  chatMessages: NodeListOf<Element>;

  constructor() {
    this.userChats = document.querySelectorAll('.user-chat');
    this.chatMessages = document.querySelectorAll('.content-chat-message-user');
  }

  ngOnInit(): void {
    this.userChats = document.querySelectorAll('.user-chat');
    this.chatMessages = document.querySelectorAll('.content-chat-message-user');

    // Activer le premier élément user-chat initialement
    this.userChats[0].classList.add('active');
    this.chatMessages[0].classList.add('active');
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

}
