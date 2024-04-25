import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { user } from '../models/user.model';

@Injectable({
    providedIn: 'root'
})
export class VideoCallService {
    private baseURL = "http://localhost:8080/VIDEOCALL";
    constructor(private httpClient: HttpClient ) { }

    handleNewMeeting(User:user ): void {
        const username = User.email.split('@')[0];
        const email1 = User.email;
        const email2 = User.email;
    
        // const apiUrl = `http://localhost:8080/api/v1/users/videocall/${username}/${email1}/${email2}`;
        // console.log("URL de l'API :", apiUrl);
    
        // this.httpClient.get<string>(apiUrl, { responseType: 'url' as 'json' })
        //     .subscribe(
        //         (response) => {
        //             console.log("Réponse de l'API :", response);
        //             // Assurez-vous que la réponse n'est pas vide
        //             if (response) {
        //                 // Rediriger vers la page HTML spécifiée dans la réponse
                        window.location.href = `${this.baseURL}/api/v1/users/videocall/${username}/${email1}/${email2}`;
            //         } else {
            //             console.error('URL de redirection manquante dans la réponse.');
            //         }
            //     },
            //     (error) => {
            //         console.error('Erreur lors de la redirection:', error);
            //     }
            // );
    }
}    
