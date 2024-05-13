import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Startup } from '../models/Startup.model';


@Injectable({
    providedIn: 'root'
})
export class ConversationService {
    private baseURL = "http://localhost:8080/MICROSERVICEMESSAGERIE";

    constructor(private httpClient: HttpClient ) { }

    getConversations(username: string): Observable<any> {
        return this.httpClient.get<any>(`${this.baseURL}/api/Conversation/${username}`);
      }
    }
