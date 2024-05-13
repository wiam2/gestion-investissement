import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
 

  public connection : signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:8080/MICROSERVICEMESSAGERIE/chat")
  .configureLogging(signalR.LogLevel.Information)
  .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<string[]>([]);
  public messages: any[] = [];
  public users: string[] = [];
  public Files :any[]=[];
  public Files$= new BehaviorSubject<any>([]);

 

  constructor(private http:HttpClient) {
    this.start();
  
    this.connection.on("ReceiveMessage", (user: string, message: string,typemessage : string , messageTime: string)=>{
      this.messages = [...this.messages, {user, message,typemessage, messageTime} ];
      this.messages$.next(this.messages);
    });
   
    

   
   }
   public clearMessages() {
    this.messages = [];
    this.messages$.next(this.messages);
  }

  //start connection
  public async start(){
    try {
      await this.connection.start();
      console.log("Connection is established!")
    } catch (error) {
      console.log(error);
    }
  }

  //Join Room
  public async joinRoom(emeteur: string, recepteur: string){
   
    return this.connection.invoke("JoinRoom", {emeteur, recepteur})
  }


  // Send Messages
  public async sendMessage(message: string,emeteur :string ,recepteur : string ,  ){
    return this.connection.invoke("SendMessage", message, {emeteur, recepteur})
  }
/* public async sendFile(file: File, emeteur: string, recepteur: string) {
    const formData = new FormData();
    formData.append('MyFile', file, file.name); // Ajoutez le fichier
    formData.append('Emeteur', emeteur); // Ajoutez l'émetteur
    formData.append('Recepteur', recepteur);
    formData.append('FileName',file.name)
   
      return this.connection.invoke("SendFile",file, emeteur,recepteur,file.name); }*/
  
      public uploadFile(file: File, emeteur: string, recepteur: string, fileName: string): Observable<any> {
        const formData = new FormData();
        formData.append('MyFile', file);
        formData.append('Emeteur', emeteur);
        formData.append('Recepteur', recepteur);
        formData.append('NomFichier', fileName);
      
        // Envoyer la requête HTTP pour télécharger le fichier
        return this.http.post('http://localhost:8081/api/fichier/upload', formData);
      }
    
public async sendFile(emeteur: string, recepteur: string, fileName: string){
  return this.connection.invoke("SendFile", emeteur,recepteur,fileName);
}
  


  //leave
  public async leaveChat(){
    this.clearMessages();
    return this.connection.stop();
  }
  TelechargerFichier(nomfichier:string):Observable<Blob>{
    
    return this.http.get(`http://localhost:8081/api/fichier/download/Filebyname/${nomfichier}`,{  responseType: 'blob' });

  }
}
