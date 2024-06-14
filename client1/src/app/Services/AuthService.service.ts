import { Injectable } from '@angular/core';
import { HttpClient ,HttpParams} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { user } from '../models/user.model';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseURL = "http://localhost:8080/MICROS-AUTH-GUSER";

  constructor(private httpClient: HttpClient) { }

  login(credentials: { email: string; password: string }): Observable<any> {
    const loginUrl = `${this.baseURL}/api/Account/login`; // Endpoint de connexion dans ton API

    return this.httpClient.post<any>(loginUrl, credentials).pipe(
      map(response => {
        if (response && response.token) {
          localStorage.setItem('JwtToken', JSON.stringify(response.token)); // Stocke le token JWT dans le localStorage
          const jwtToken = localStorage.getItem('JwtToken');
          console.log(jwtToken);
        }
        return response;

      }),
      catchError(error => {
        return of(error);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('JwtToken');
  }

  getToken(): string {
    return JSON.parse(localStorage.getItem('JwtToken') || 'null');
  }

  isAuthenticated(): boolean {
    return this.getToken() != null;
  }


  /* hasRole(role: string): boolean {
     const currentUser = this.currentUser();
     return !!currentUser && currentUser.role === role;
   }

   getHeaders(): HttpHeaders {
     const token = this.getToken();
     const headers = new HttpHeaders({
       'Content-Type': 'application/json',
       'Access-Control-Allow-Origin': 'http://localhost:4200',
       'Access-Control-Allow-Credentials': 'true',
       'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, DELETE',
       'Access-Control-Max-Age': '3600',
       'Access-Control-Allow-Headers': 'Content-Type, Accept, X-Requested-With, remember-me',
       'Authorization': `Bearer ${token}`
     });
     return headers;
   }*/


  currentUser(): user | null {
    if (this.getToken()) {
      const tokenPayload: any = jwtDecode(this.getToken());
      if (tokenPayload) {
        const user: user = {

          id: tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
          email: tokenPayload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
          password: '',
          confirmPassword: '',// Il est généralement préférable de ne pas stocker le mot de passe dans l'objet User côté client.
        };
        return user;
      }
    }
    return null;
  }
  currentUserRole(): string | null {
    const token = this.getToken();

    if (token) {
      const tokenPayload: any = jwtDecode(token);

      if (tokenPayload) {
        const role: string = tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        return role;
      }
    }

    return null;
  }

  forgetPassword(email:string): Observable<Object> {
    const params = new HttpParams().set('email', email);
    // Corrected the URL by enclosing it in quotes and fixing the path
    return this.httpClient.post(`${this.baseURL}/api/Account/forget_password`,{},{params});
  }
  autoLogout(dateExpiration: number): void {
    setTimeout(() => {
      this.logout();
    }, dateExpiration);
  }

}
