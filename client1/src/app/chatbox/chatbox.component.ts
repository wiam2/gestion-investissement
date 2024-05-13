
import { VideoCallService } from '../Services/VideoCallService';
import { AuthService } from '../Services/AuthService.service';
import { user } from '../models/user.model';
import { ChatService } from '../Services/chat.service';
import { AfterViewChecked, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, inject ,Input} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConversationService} from '../Services/conversation.service';
@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css']
})
export class ChatboxComponent implements OnInit {
  router = inject(Router);
  userChats: NodeListOf<Element>;
  chatMessages: NodeListOf<Element>;
  messages: any[] = [];
  chatService = inject(ChatService);
  me  = sessionStorage.getItem("me");
 mycontact:any;
  selectedFile: File | null = null; 
  @ViewChild('scrollMe') private scrollContainer!: ElementRef;
  inputMessage :string = "";
  fichierDownoload : boolean = false ;
  conversations: any;
 
  
 
  useremail:string ='';
  click:boolean=false;

  constructor(private videoCallService:VideoCallService, private authService :AuthService , private conversationService :ConversationService ,private cdr:  ChangeDetectorRef,public route: ActivatedRoute ) {
    this.userChats = document.querySelectorAll('.user-chat');
    this.chatMessages = document.querySelectorAll('.content-chat-message-user');
  }

  ngOnInit(): void {
   
    this.mycontact = sessionStorage.getItem("contact") ;
    
    
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
onFileSelected(event: any) {
  this.selectedFile = event.target.files[0];
}
send() {
  console.log(this.inputMessage);
  if (this.selectedFile) {
    // Envoyer le fichier
    this.sendFile();
    this.sendfile2();
  } else {
    // Envoyer le message
    this.sendMessage();
  }
}

sendFile() {
  if (this.selectedFile && this.me && this.mycontact ) {
    if(sessionStorage.getItem("contact"))
      this.mycontact  = sessionStorage.getItem("contact");
    this.chatService.uploadFile(this.selectedFile, this.me, this.mycontact, this.selectedFile.name).subscribe(
      data => {
        console.log('Fichier envoyé avec succès.');
      
      },
      err => {
        console.error(err);
      }
    );
  } else {
    console.error('Veuillez sélectionner un fichier et spécifier le nom de l\'utilisateur et la salle.');
  }
}
sendfile2(){ 
  if (this.selectedFile && this.me && this.mycontact){
    if(sessionStorage.getItem("contact"))
      this.mycontact  = sessionStorage.getItem("contact");
  this.chatService.sendFile(this.me, this.mycontact, this.selectedFile.name)
  .then(() => {
    this.inputMessage = '';
  })
  .catch(err => {
    console.error(err);
  });}}

ngAfterViewChecked(): void {
  this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
}

sendMessage(){
  if(sessionStorage.getItem("contact"))
    this.mycontact  = sessionStorage.getItem("contact");
  if(this.me && this.mycontact)
    
  this.chatService.sendMessage(this.inputMessage,this.me,this.mycontact)
  .then(()=>{
    this.inputMessage = '';
  }).catch((err)=>{
    console.log(err);
  })
}

leaveChat(){
  this.chatService.leaveChat()
  .then(()=>{
    this.router.navigate(['welcome']);
    setTimeout(() => {
      location.reload();
    }, 0);
  }).catch((err)=>{
    console.log(err);
  })
}
generateDownload1Link(fichierId: string): void {
  this.chatService.TelechargerFichier(fichierId).subscribe(
    (response: any) => {
      const blob = new Blob([response], { type: 'application/octet-stream' });
      const url = window.URL.createObjectURL(blob);
    
      const a = document.createElement('a');
      a.href = url;
     
      a.download = fichierId; // Remplacez par le nom de fichier 
     
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
     
      this.fichierDownoload = true ;
    },
    error => {
      console.error('Erreur lors du téléchargement du fichier', error);
    }
  );
}

leave(){
  
  this.chatService.leaveChat();
  this.router.navigate(['conversation']);
}


}
