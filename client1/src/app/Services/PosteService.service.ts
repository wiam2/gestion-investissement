import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { posteInv } from '../models/posteInv.model';
import { postestar } from '../models/postestar.model';
@Injectable({
  providedIn: 'root'
})
export class PosteService {

  private baseURL = "http://localhost:8080/MICROS-POSTES";

  constructor(private httpClient: HttpClient) { }

  CreatePosteInv(posteInv: posteInv): Observable<Object> {
    // Corrected the URL by enclosing it in quotes and fixing the path
    return this.httpClient.post(`${this.baseURL}/api/PosteInv/CreatePoste`, posteInv);
  }
  CreatePosteStar(posteStar: postestar): Observable<Object> {
    // Corrected the URL by enclosing it in quotes and fixing the path
    return this.httpClient.post(`${this.baseURL}/api/PosteStar/CreatePoste`, posteStar);
  }

  deletePosteInv(id: number): Observable<any> {
    console.log(id);
    return this.httpClient.delete(`${this.baseURL}/api/PosteInv/Delete/${id}`);
  }
  deletePosteStar(id: number): Observable<any> {
    return this.httpClient.delete(`${this.baseURL}/api/PosteStar/Delete/${id}`);
  }
  getPostsInv(id: string): Observable<posteInv[]> {
    // Corrected the URL by enclosing it in quotes and fixing the path
    return this.httpClient.get<posteInv[]>(`${this.baseURL}/api/PosteInv/getPosteByUserId/${id}`,);
  }
  getPostsStar(id: string): Observable<postestar[]> {
    // Corrected the URL by enclosing it in quotes and fixing the path
    return this.httpClient.get<postestar[]>(`${this.baseURL}/api/PosteStar/getPosteByUserId/${id}`,);
  }
  UpdatePosteInv(id: number, poste:posteInv): Observable<Object> {

    return this.httpClient.put(`${this.baseURL}/api/PosteInv/UpdatePoste/${id}`, poste);
}
UpdatePosteStar(id: number, poste:postestar): Observable<Object> {

  return this.httpClient.put(`${this.baseURL}/api/PosteStar/UpdatePoste/${id}`, poste);
}
ValiderInv(id: string): Observable<Object> {
  // Corrected the URL by enclosing it in quotes and fixing the path
  return this.httpClient.patch(`${this.baseURL}/api/PosteInv/ValiderPoste/${id}`,{});
}
ValiderStar(id: string): Observable<Object> {
  // Corrected the URL by enclosing it in quotes and fixing the path
  return this.httpClient.patch(`${this.baseURL}/api/PosteStar/ValiderPoste/${id}`,{});
}
getPostsForInv(): Observable<any[]> {
  // Corrected the URL by enclosing it in quotes and fixing the path
  return this.httpClient.get<any[]>(`${this.baseURL}/api/PosteStar/AllPostesDecsending`);
}
getPostsForStar(): Observable<any[]> {
  // Corrected the URL by enclosing it in quotes and fixing the path
  return this.httpClient.get<[]>(`${this.baseURL}/api/PosteInv/AllPostesDecsending`);
}
}


