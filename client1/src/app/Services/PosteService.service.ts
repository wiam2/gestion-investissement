import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable , of} from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { posteInv} from '../models/posteInv.model';
import { postestar } from '../models/postestar.model';
@Injectable({
    providedIn: 'root'
})
export class PosteService {

    private baseURL = "http://localhost:8080/MICROS-POSTES";

    constructor(private httpClient: HttpClient ) { }

    CreatePosteInv(posteInv:posteInv): Observable<Object> {
      // Corrected the URL by enclosing it in quotes and fixing the path
      return this.httpClient.post(`${this.baseURL}/api/PosteInv/CreatePoste`, posteInv);
  }
  CreatePosteStar(posteStar:postestar): Observable<Object> {
    // Corrected the URL by enclosing it in quotes and fixing the path
    return this.httpClient.post(`${this.baseURL}/api/PosteStar/CreatePoste`, posteStar);
}
  
   deletePosteInv(id: number): Observable<any>{
    return this.httpClient.delete(`${this.baseURL}/api/PosteStar/Delete/${id}`);
  }
  deletePosteStar(id: number): Observable<any>{
    return this.httpClient.delete(`${this.baseURL}/api/PosteInv/Delete/${id}`);
  }
}

    

    